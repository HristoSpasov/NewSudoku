namespace NewSudoku.Repository.Source
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Xml.Serialization;

    public class XMLSet<TModel> where TModel : class
    {
        private string filename;
        private ICollection<TModel> models;

        public XMLSet(string fileName)
        {
            this.filename = fileName;
        }

        public ICollection<TModel> Data
        {
            get
            {
                try
                {
                    if (this.models == null)
                    {
                        this.Load();
                    }
                }
                catch (Exception)
                {
                    this.models = new List<TModel>();
                }

                return this.models;
            }

            set
            {
                this.models = value;
                this.Save();
            }
        }

        public void Save()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));

            using (StreamWriter sw = new StreamWriter(this.filename))
            {
                serializer.Serialize(sw, this.models);
            }
        }

        public void Load()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<TModel>));

            using (StreamReader sr = new StreamReader(this.filename))
            {
                this.models = serializer.Deserialize(sr) as List<TModel>;
            }
        }
    }
}