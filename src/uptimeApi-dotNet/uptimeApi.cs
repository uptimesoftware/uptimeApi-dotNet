using System.IO;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;


namespace uptime
{
    // uptimeApi.cs
    public class uptimeApi
    {
        private string _apiHostname;
        readonly string _apiUsername;
        readonly string _apiPassword;
        readonly int _apiPort;
        readonly string _apiVersion;
        readonly bool _apiSSL;

        public uptimeApi(string username, string password, string hostname, int port = 9997, string version = "v1", bool ssl = true)
        {
            _apiUsername = username;
            _apiPassword = password;
            _apiHostname = hostname;
            _apiPort = port;
            _apiVersion = version;
            _apiSSL = ssl;
        }


        public JObject getApiInfo(ref string error)
        {
            string data = getJSON(ref error, "", false);
            return parseJSON(data, ref error);
        }
        public JArray getElements(ref string error, string filter)
        {
            string apiResource = "elements";
            string data = getJSON(ref error, apiResource);
            JArray output = parseJSONArray(data, ref error);
            return output;
        }
        public JArray getMonitors(ref string error, string filter)
        {
            string apiResource = "monitors";
            string data = getJSON(ref error, apiResource);
            JArray output = parseJSONArray(data, ref error);
            return output;
        }
        public JArray getGroups(ref string error, string filter)
        {
            string apiResource = "groups";
            string data = getJSON(ref error, apiResource);
            JArray output = parseJSONArray(data, ref error);
            return output;
        }


        public string getJSON(ref string error, string apiResource, bool addVersionToUrl = true)
        {
            // clear error string
            error = "";
            string output = "";

            // setup the baseURL
            string protocol = "https://";
            if (!_apiSSL) { protocol = "http://"; }
            string baseURL = protocol + _apiHostname + ":" + _apiPort + "/api";
            if (addVersionToUrl)
            {
                baseURL += "/" + _apiVersion + "/";
            }
            baseURL += apiResource;

            try
            {
                // connect to the API
                WebClient wc = new WebClient();
                wc.Credentials = new NetworkCredential(_apiUsername, _apiPassword);
                // ignore the SSL certificate warning
                ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                // get the output
                output = wc.DownloadString(baseURL);
            }
            catch (Exception ex)
            {
                error = "Error: " + ex.Message;
            }


            return output;
        }

        protected JObject parseJSON(string data, ref string error)
        {
            JObject jarr = new JObject();
            // if there was an error earlier, don't bother parsing anything
            if (data.Length > 0 && error.Length == 0)
            {
                try
                {
                    // single object
                    jarr = JObject.Parse(data);
                }
                catch (Exception ex)
                {
                    error = "Error: " + ex.Message;
                }
            }
            return jarr;
        }
        protected JArray parseJSONArray(string data, ref string error)
        {
            JArray jarr = new JArray();
            // if there was an error earlier, don't bother parsing anything
            if (data.Length > 0 && error.Length == 0)
            {
                try
                {
                    // array of objects
                    jarr = JArray.Parse(data);
                }
                catch (Exception ex)
                {
                    error = "Error: " + ex.Message;
                }
            }
            return jarr;
        }
    }
}
