// This file was generated by the Gtk# code generator.
// Any changes made will be lost if regenerated.

namespace Gst {

	using System;

	public delegate void FeatureAddedHandler(object o, FeatureAddedArgs args);

	public class FeatureAddedArgs : Gst.GLib.SignalArgs {
		public Gst.PluginFeature Feature{
			get {
				return (Gst.PluginFeature) Args [0];
			}
		}

	}
}
