using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WindowsFormsApp1.Impl;

namespace WindowsFormsApp1.Storages {
    public class Storage {
        private const string _LogFile = "Log.txt";
        public const string _JsonFile = "UniversityData.json";
        public University NewUniversity = new University();

        public void SerializeToJson() {
            try {

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string data = serializer.Serialize(NewUniversity);

                string path = Path.Combine(Environment.CurrentDirectory, _JsonFile);
                File.WriteAllText(path, data);
            }
            catch (Exception ex) {
         
                MessageBox.Show("Something wrong happened! Please send me the log file!", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                string path = Path.Combine(Environment.CurrentDirectory, _LogFile);
                File.WriteAllText(path, ex.ToString());
            }

        }

        public void DeserializeFromJson() {
            try {

                JavaScriptSerializer serializer = new JavaScriptSerializer();

                string path = Path.Combine(Environment.CurrentDirectory, _JsonFile);

                if (File.Exists(path)) {
                    string data = File.ReadAllText(path);

                    NewUniversity = serializer.Deserialize<University>(data);
                }


            }
            catch (Exception ex) {

                MessageBox.Show("Something wrong happened! Please send me the log file!", "Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);

                string path = Path.Combine(Environment.CurrentDirectory, _LogFile);
                File.WriteAllText(path, ex.ToString());
            }

        }

    }
}
