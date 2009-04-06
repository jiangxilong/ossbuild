from wl import *

s_params["13_5"]="""
/// Boundaries (depends on wavelet)
/// This much is reserved at the sides of the signal
/// Must be even!
#define BLEFT 2
#define BRIGHT 2

/// Initial shift (to keep precision in integer wavelets)
#define INITIAL_SHIFT 1
#define INITIAL_OFFSET 1

#define STAGE1_OFFSET 7
#define STAGE1_SHIFT 4

#define STAGE2_OFFSET 16
#define STAGE2_SHIFT 5

/// Vertical pass row management
#define RLEFT 3
#define RRIGHT 2
#define COPYROWS 3
"""

s_transform_h["13_5"] = """
    if(tidu16==0)
    {
        shared[half+BLEFT-2] = shared[half+BLEFT];
        shared[half+BLEFT-1] = shared[half+BLEFT];
        shared[half+BLEFT+(width>>1)] = shared[half+BLEFT+(width>>1)-1];
    }

    __syncthreads();
    
    // Now apply wavelet lifting to entire line at once
    // Process odd
    const int end = BLEFT+(width>>1);
    for(ofs = BLEFT+tidu16; ofs < end; ofs += BSH)
    {
        int acc = STAGE2_OFFSET;

        acc += -1*shared[half+ofs-2];
        acc += __mul24(9,shared[half+ofs-1]);
        acc += __mul24(9,shared[half+ofs+0]);
        acc += -1*shared[half+ofs+1];
        
        shared[ofs] -= acc >> STAGE2_SHIFT;
    }


    __syncthreads();

    if(tidu16==0)
    {    
        shared[BLEFT-1] = shared[BLEFT];
        shared[BLEFT+(width>>1)] = shared[BLEFT+(width>>1)-1];
        shared[BLEFT+(width>>1)+1] = shared[BLEFT+(width>>1)-1];
    }

    __syncthreads();
    
    // Process even
    for(ofs = BLEFT+tidu16; ofs < end; ofs += BSH)
    {
        int acc = STAGE1_OFFSET;

        acc += 1*shared[ofs-1];
        acc += __mul24(-9, shared[ofs+0]);
        acc += __mul24(-9, shared[ofs+1]);
        acc += 1*shared[ofs+2];
        
        shared[ofs + half] -= acc >> STAGE1_SHIFT;
    }

    __syncthreads();
"""

s_transform_v["13_5"] = """
__device__ void doTransform(int xofs)
{
    const int tidx = (threadIdx.x<<1)+xofs;   // column
    const int tidy = threadIdx.y;   // row

    extern __shared__ DATATYPE shared[];
    int ofs;

    ofs = ((RLEFT+(tidy<<1)+3)<<BCOLS_SHIFT) + tidx;
    {
        int acc = STAGE2_OFFSET;

        acc += -1*shared[ofs-3*BCOLS];
        acc += __mul24(9,shared[ofs-BCOLS]);
        acc += __mul24(9,shared[ofs+BCOLS]);
        acc += -1*shared[ofs+3*BCOLS];
        
        shared[ofs] -= acc >> STAGE2_SHIFT;
    }
    
    __syncthreads();
    
    ofs -= BCOLS*3;
    
    {
        int acc = STAGE1_OFFSET;

        acc += 1*shared[ofs-3*BCOLS];
        acc += __mul24(-9, shared[ofs-BCOLS]);
        acc += __mul24(-9, shared[ofs+BCOLS]);
        acc += 1*shared[ofs+3*BCOLS];

        shared[ofs] -= acc >> STAGE1_SHIFT;
    }

}

__device__ void doTransformTB(int xofs, unsigned int leftover)
{
    const int tidx = (threadIdx.x<<1)+xofs;   // column
    const int tidy = threadIdx.y;   // row
    const int minn = (RLEFT<<BCOLS_SHIFT) + tidx;
    const int maxx = leftover-(2<<BCOLS_SHIFT) + tidx;
    
    extern __shared__ DATATYPE shared[];
    int ofs;

    /// Do the transform
    /// Do procesing on shared mem
    /// Treat all columns the same
    
    /// Process even rows
    ofs = ((RLEFT+(tidy<<1))<<BCOLS_SHIFT) + tidx;
    {
        int acc = STAGE2_OFFSET;

        acc += -1*shared[max(ofs-3*BCOLS, minn+BCOLS)];
        acc += __mul24(9,shared[max(ofs-BCOLS, minn+BCOLS)]);
        acc += __mul24(9,shared[ofs+BCOLS]);
        acc += -1*shared[min(ofs+3*BCOLS, maxx+BCOLS)];

        shared[ofs] -= acc >> STAGE2_SHIFT;
    }
    
    __syncthreads();
    
    /// Process odd rows
    ofs += BCOLS;
    {
        int acc = STAGE1_OFFSET;

        acc += 1*shared[max(ofs-3*BCOLS,minn)];
        acc += __mul24(-9, shared[ofs-BCOLS]);
        acc += __mul24(-9, shared[min(ofs+BCOLS,maxx)]);
        acc += 1*shared[min(ofs+3*BCOLS,maxx)];

        shared[ofs] -= acc >> STAGE1_SHIFT;
    }

}

__device__ void doTransformT(int xofs)
{
    const int tidx = (threadIdx.x<<1)+xofs;   // column
    const int tidy = threadIdx.y;   // row
    const int minn = ((RLEFT+SKIPTOP)<<BCOLS_SHIFT) + tidx;
    
    extern __shared__ DATATYPE shared[];
    int ofs;

    /// Do the transform
    
    /// Do procesing on shared mem
    /// Treat all columns the same
    ofs = ((SKIPTOP+RLEFT+(tidy<<1))<<BCOLS_SHIFT) + tidx;
    
    /// Process even rows
    {
        int acc = STAGE2_OFFSET;

        acc += -1*shared[max(ofs-3*BCOLS, minn+BCOLS)];
        acc += __mul24(9,shared[max(ofs-BCOLS, minn+BCOLS)]);
        acc += __mul24(9,shared[ofs+BCOLS]);
        acc += -1*shared[ofs+3*BCOLS];
        
        shared[ofs] -= acc >> STAGE2_SHIFT;
    }

    __syncthreads();

    /// Process odd rows, except for last
    ofs += BCOLS;
    if(tidy < (BSVY-2))
    {
        int acc = STAGE1_OFFSET;

        acc += 1*shared[max(ofs-3*BCOLS,minn)];
        acc += __mul24(-9, shared[ofs-BCOLS]);
        acc += __mul24(-9, shared[ofs+BCOLS]);
        acc += 1*shared[ofs+3*BCOLS];
        
        shared[ofs] -= acc >> STAGE1_SHIFT;
    }

}

// Process leftover
__device__ void doTransformB(int xofs, unsigned int leftover)
{
    const int tidx = (threadIdx.x<<1)+xofs;   // column
    const int tidy = threadIdx.y;   // row
    const int maxx = leftover-(2<<BCOLS_SHIFT) + tidx;
    
    extern __shared__ DATATYPE shared[];
    int ofs, ofs_t;

    ofs = ((RLEFT+(tidy<<1))<<BCOLS_SHIFT) + tidx;

    for(ofs_t=ofs+3*BCOLS; ofs_t<leftover; ofs_t += BSVY*2*BCOLS)
    {
        int acc = STAGE2_OFFSET;

        acc += -1*shared[ofs_t-3*BCOLS];
        acc += __mul24(9,shared[ofs_t-BCOLS]);
        acc += __mul24(9,shared[ofs_t+BCOLS]);
        acc += -1*shared[min(ofs_t+3*BCOLS, maxx+BCOLS)];
        
        shared[ofs_t] -= acc >> STAGE2_SHIFT;
    }
    
    __syncthreads();
    
    for(ofs_t=ofs; ofs_t<leftover; ofs_t += BSVY*2*BCOLS)
    {
        int acc = STAGE1_OFFSET;

        acc += 1*shared[ofs_t-3*BCOLS];
        acc += __mul24(-9, shared[ofs_t-BCOLS]);
        acc += __mul24(-9, shared[min(ofs_t+BCOLS,maxx)]);
        acc += 1*shared[min(ofs_t+3*BCOLS,maxx)];
        
        shared[ofs_t] -= acc >> STAGE1_SHIFT;
    }
}

"""

