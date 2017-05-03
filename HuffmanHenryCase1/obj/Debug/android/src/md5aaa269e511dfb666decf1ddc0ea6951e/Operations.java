package md5aaa269e511dfb666decf1ddc0ea6951e;


public class Operations
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("HuffmanHenryCase1.Operations, HuffmanHenryCase1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Operations.class, __md_methods);
	}


	public Operations () throws java.lang.Throwable
	{
		super ();
		if (getClass () == Operations.class)
			mono.android.TypeManager.Activate ("HuffmanHenryCase1.Operations, HuffmanHenryCase1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
