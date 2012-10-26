uptimeApi-dotNet
================
API helper class for up.time 7.1+ API. This makes using the up.time API simple and powerful.
This also includes a Windows tool you can use to verify the up.time API is working properly.

This was created on Visual Studio 2010 with .NET 4.0.

Requirements
------------
* Newtonsoft.Json .Net Library [http://james.newtonking.com/pages/json-net.aspx]

Setup
-----
Create a .Net project and import the following resource files (DLL):
* Newtonsoft.Json.dll
* uptimeApi.dll

In your code, import the necessary resources:

	using Newtonsoft.Json.Linq;
	using uptime;

Then initialize the uptimeApi class and call the functions you require:

	uptimeApi api = new uptimeApi(txtUsername.Text, txtPassword.Text, txtHostname.Text, Int32.Parse(txtPort.Text), txtVersion.Text, chkSSL.Checked);
	
	JArray allElements = api.getElements(ref error, "");
	txtOutput.Text += "Elements      : ";
	if (error.Length > 0) { txtOutput.Text = error + Environment.NewLine; }
	else { txtOutput.Text += "Successfully retrieved info on " + allElements.Count + " Elements." + Environment.NewLine; }

Happy Coding!