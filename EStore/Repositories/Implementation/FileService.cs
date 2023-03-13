using EStore.Repositories.Abstract;

namespace EStore.Repositories.Implementation
{
	public class FileService : IFileService
	{
		private readonly IWebHostEnvironment webHostEnvironment;
		public FileService(IWebHostEnvironment webHostEnvironment)
		{
			this.webHostEnvironment = webHostEnvironment;
		}

        public Tuple<int, string> SaveImage(IFormFile imageFile)
        {
            try
            {
                string wwwPath = this.webHostEnvironment.WebRootPath;
                string path = Path.Combine(wwwPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Check the allowed extenstions
                string ext = Path.GetExtension(imageFile.FileName);
                string[] allowedExtensions = new string[] { ".jpg", ".png", ".jpeg" };
                if (!allowedExtensions.Contains(ext))
                {
                    string msg = string.Format("Only {0} extensions are allowed", string.Join(",", allowedExtensions));
                    return new Tuple<int, string>(0, msg);
                }
                string uniqueString = Guid.NewGuid().ToString();
                // we are trying to create a unique filename here
                string newFileName = uniqueString + ext;
                string fileWithPath = Path.Combine(path, newFileName);
                FileStream stream = new FileStream(fileWithPath, FileMode.Create);
                imageFile.CopyTo(stream);
                stream.Close();
                return new Tuple<int, string>(1, newFileName);
            }
            catch (Exception ex)
            {
                return new Tuple<int, string>(0, "Error has occured");
            }
        }

        public bool DeleteImage(string imageFileName)
        {
            try
            {
                string wwwPath = this.webHostEnvironment.WebRootPath;
                string path = Path.Combine(wwwPath, "Uploads\\", imageFileName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
