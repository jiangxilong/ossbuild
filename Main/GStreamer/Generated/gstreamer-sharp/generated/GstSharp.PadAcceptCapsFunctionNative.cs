// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace GstSharp {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[UnmanagedFunctionPointer (CallingConvention.Cdecl)]
	internal delegate bool PadAcceptCapsFunctionNative(IntPtr pad, IntPtr caps);

	internal class PadAcceptCapsFunctionInvoker {

		PadAcceptCapsFunctionNative native_cb;
		IntPtr __data;
		Gst.GLib.DestroyNotify __notify;

		~PadAcceptCapsFunctionInvoker ()
		{
			if (__notify == null)
				return;
			__notify (__data);
		}

		internal PadAcceptCapsFunctionInvoker (PadAcceptCapsFunctionNative native_cb) : this (native_cb, IntPtr.Zero, null) {}

		internal PadAcceptCapsFunctionInvoker (PadAcceptCapsFunctionNative native_cb, IntPtr data) : this (native_cb, data, null) {}

		internal PadAcceptCapsFunctionInvoker (PadAcceptCapsFunctionNative native_cb, IntPtr data, Gst.GLib.DestroyNotify notify)
		{
			this.native_cb = native_cb;
			__data = data;
			__notify = notify;
		}

		internal Gst.PadAcceptCapsFunction Handler {
			get {
				return new Gst.PadAcceptCapsFunction(InvokeNative);
			}
		}

		bool InvokeNative (Gst.Pad pad, Gst.Caps caps)
		{
			bool result = native_cb (pad == null ? IntPtr.Zero : pad.Handle, caps == null ? IntPtr.Zero : caps.Handle);
			return result;
		}
	}

	internal class PadAcceptCapsFunctionWrapper {

		public bool NativeCallback (IntPtr pad, IntPtr caps)
		{
			try {
				bool __ret = managed (Gst.GLib.Object.GetObject(pad) as Gst.Pad, caps == IntPtr.Zero ? null : (Gst.Caps) Gst.GLib.Opaque.GetOpaque (caps, typeof (Gst.Caps), false));
				if (release_on_call)
					gch.Free ();
				return __ret;
			} catch (Exception e) {
				Gst.GLib.ExceptionManager.RaiseUnhandledException (e, false);
				return false;
			}
		}

		bool release_on_call = false;
		GCHandle gch;

		public void PersistUntilCalled ()
		{
			release_on_call = true;
			gch = GCHandle.Alloc (this);
		}

		internal PadAcceptCapsFunctionNative NativeDelegate;
		Gst.PadAcceptCapsFunction managed;

		public PadAcceptCapsFunctionWrapper (Gst.PadAcceptCapsFunction managed)
		{
			this.managed = managed;
			if (managed != null)
				NativeDelegate = new PadAcceptCapsFunctionNative (NativeCallback);
		}

		public static Gst.PadAcceptCapsFunction GetManagedDelegate (PadAcceptCapsFunctionNative native)
		{
			if (native == null)
				return null;
			PadAcceptCapsFunctionWrapper wrapper = (PadAcceptCapsFunctionWrapper) native.Target;
			if (wrapper == null)
				return null;
			return wrapper.managed;
		}
	}
#endregion
}
