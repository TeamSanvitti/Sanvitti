using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class Maker
    {
        private int _makerGUID;
        private string _makerName;
        private string _makerDescription;
        private string _makerImage;
        private string _makerComments;
        private int _parentGUID;
        private int _active;
        private int _isalphabet;

        public int MakerGUID
        {
            get
            {
                return _makerGUID;
            }
            set
            {
                _makerGUID = value;
            }
        }
        public int Isalphabet
        {
            get
            {
                return _isalphabet;
            }
            set
            {
                _isalphabet = value;
            }
        }

        public string MakerName
        {
            get
            {
                return _makerName;
            }
            set
            {
                _makerName = value;
            }
        }

        public string MakerDescription
        {
            get
            {
                return _makerDescription;
            }
            set
            {
                _makerDescription = value;
            }
        }

        public string MakerImage
        {
            get
            {
                return _makerImage;
            }
            set
            {
                _makerImage = value;
            }
        }

        public string MakerComments
        {
            get
            {
                return _makerComments;
            }
            set
            {
                _makerComments = value;
            }
        }

        public int ParentGUID
        {
            get
            {
                return _parentGUID;
            }
            set
            {
                _parentGUID = value;
            }
        }

        public int isActive
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
    }
}
