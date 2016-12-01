using Dicom;
using Dicom.Imaging;
using Dicom.IO;
using PaintDotNet;
using PaintDotNet.Controls;
using PaintDotNet.Data;
using PaintDotNet.Imaging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DicomFileTypePlugin
{
    public class DicomFileType : FileType
    {
        public DicomFileType()
            : base("Dicom Document",
                FileTypeFlags.SupportsLoading | FileTypeFlags.SupportsSaving,
                new String[] { ".dcm" })
        {
        }

        protected override Document OnLoad(Stream input)
        {
            try
            {   
                var file = DicomFile.Open(input);
                var image = new DicomImage(file.Dataset);

                return Document.FromImage(image.RenderImage().As<System.Drawing.Bitmap>());
            }
            catch(Exception e)
            {
                MessageBox.Show("Problem Importing File. "+ e.ToString());

                System.Drawing.Bitmap b = new System.Drawing.Bitmap(500, 500);
                return Document.FromImage(b);
            }
        }

        protected override void OnSave(Document input, Stream output, SaveConfigToken token,
            Surface scratchSurface, ProgressEventHandler callback)
        {
            RenderArgs ra = new RenderArgs(new Surface(input.Size));
            
        }
    }

    public class DicomFileTypeFactory : IFileTypeFactory
    {
        public FileType[] GetFileTypeInstances()
        {
            return new FileType[] { new DicomFileType() };
        }
    }
}
