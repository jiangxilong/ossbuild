// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;
	using System.Runtime.InteropServices;

#region Autogenerated code
	[Flags]
	[Gst.GLib.GType (typeof (Gst.IndexFlagsGType))]
	public enum IndexFlags {

		Writable = ObjectFlags.Last << 0,
		Readable = ObjectFlags.Last << 1,
		Last = ObjectFlags.Last << 8,
	}

	internal class IndexFlagsGType {
		[DllImport ("libgstreamer-0.10.dll", CallingConvention = CallingConvention.Cdecl)]
		static extern IntPtr gst_index_flags_get_type ();

		public static Gst.GLib.GType GType {
			get {
				return new Gst.GLib.GType (gst_index_flags_get_type ());
			}
		}
	}
#endregion
}
