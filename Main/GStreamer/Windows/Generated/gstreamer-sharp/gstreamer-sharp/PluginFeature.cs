// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Collections;
	using System.Runtime.InteropServices;

#region Autogenerated code
	public partial class PluginFeature : Gst.Object {

		public PluginFeature(IntPtr raw) : base(raw) {}

		protected PluginFeature() : base(IntPtr.Zero)
		{
			CreateNativeObject (new string [0], new Gst.GLib.Value [0]);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern uint gst_plugin_feature_get_rank(IntPtr raw);

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_plugin_feature_set_rank(IntPtr raw, uint rank);

		public uint Rank {
			get  {
				uint raw_ret = gst_plugin_feature_get_rank(Handle);
				uint ret = raw_ret;
				return ret;
			}
			set  {
				gst_plugin_feature_set_rank(Handle, value);
			}
		}

		[StructLayout (LayoutKind.Sequential)]
		struct GstPluginFeatureClass {
			[MarshalAs (UnmanagedType.ByValArray, SizeConst=4)]
			public IntPtr[] GstReserved;
		}

		static uint class_offset = ((Gst.GLib.GType) typeof (Gst.Object)).GetClassSize ();
		static Hashtable class_structs;

		static GstPluginFeatureClass GetClassStruct (Gst.GLib.GType gtype, bool use_cache)
		{
			if (class_structs == null)
				class_structs = new Hashtable ();

			if (use_cache && class_structs.Contains (gtype))
				return (GstPluginFeatureClass) class_structs [gtype];
			else {
				IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
				GstPluginFeatureClass class_struct = (GstPluginFeatureClass) Marshal.PtrToStructure (class_ptr, typeof (GstPluginFeatureClass));
				if (use_cache)
					class_structs.Add (gtype, class_struct);
				return class_struct;
			}
		}

		static void OverrideClassStruct (Gst.GLib.GType gtype, GstPluginFeatureClass class_struct)
		{
			IntPtr class_ptr = new IntPtr (gtype.GetClassPtr ().ToInt64 () + class_offset);
			Marshal.StructureToPtr (class_struct, class_ptr, false);
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_plugin_feature_get_name(IntPtr raw);

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern void gst_plugin_feature_set_name(IntPtr raw, IntPtr name);

		public string FeatureName { 
			get {
				IntPtr raw_ret = gst_plugin_feature_get_name(Handle);
				string ret = Gst.GLib.Marshaller.Utf8PtrToString (raw_ret);
				return ret;
			}
			set {
				IntPtr native_value = Gst.GLib.Marshaller.StringToPtrGStrdup (value);
				gst_plugin_feature_set_name(Handle, native_value);
				Gst.GLib.Marshaller.Free (native_value);
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern bool gst_plugin_feature_check_version(IntPtr raw, uint min_major, uint min_minor, uint min_micro);

		public bool CheckVersion(uint min_major, uint min_minor, uint min_micro) {
			bool raw_ret = gst_plugin_feature_check_version(Handle, min_major, min_minor, min_micro);
			bool ret = raw_ret;
			return ret;
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_plugin_feature_get_type();

		public static new Gst.GLib.GType GType { 
			get {
				IntPtr raw_ret = gst_plugin_feature_get_type();
				Gst.GLib.GType ret = new Gst.GLib.GType(raw_ret);
				return ret;
			}
		}

		[DllImport("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_plugin_feature_load(IntPtr raw);

		public Gst.PluginFeature Load() {
			IntPtr raw_ret = gst_plugin_feature_load(Handle);
			Gst.PluginFeature ret = Gst.GLib.Object.GetObject(raw_ret, true) as Gst.PluginFeature;
			return ret;
		}

#endregion
	}
}
