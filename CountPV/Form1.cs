using System;
using System.IO;
using System.Windows.Forms;
namespace CountPV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += Form1_DragEnter;
            this.DragDrop += Form1_DragDrop;

        }
        private string _folderPath;

        //文件夹拖入
        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];

                if (Directory.Exists(path) || File.Exists(path))
                {
                    e.Effect = DragDropEffects.Copy;
                }
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
                txtFolderPath.Text = path;
                _folderPath = path;
            }
            txtLog.AppendText($"已选择文件夹【{_folderPath}】\r\n");

        }

        private Dictionary<string, string> _renamedFolders; // 用于保存旧名称和新名称的映射

        private void btnSelect_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                _folderPath = folderDialog.SelectedPath;
                txtFolderPath.Text = _folderPath;
            }
            txtLog.AppendText($"已选择文件夹【{_folderPath}】\r\n");
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            txtLog.SelectionFont = new Font(txtLog.Font, FontStyle.Bold); // 设置为粗体
            txtLog.SelectionColor = Color.Blue;
            txtLog.AppendText($"【{_folderPath}】 正在处理中.....\r\n");
            if (!string.IsNullOrEmpty(_folderPath))
            {
                // 获取文件夹中的子文件夹
                string[] subDirs = Directory.GetDirectories(_folderPath);

                // 遍历子文件夹，统计和重命名文件夹
                _renamedFolders = new Dictionary<string, string>(); // 初始化字典
                foreach (string subDir in subDirs)
                {
                    int imageCount, videoCount, txtCount;
                    CountImagesAndVideos(subDir, out imageCount, out videoCount, out txtCount);

                    string originalName = Path.GetFileName(subDir);
                    string newName = $"{originalName} [{imageCount}P-{videoCount}V-{videoCount}T={GetFolderSizeString(subDir)}]";

                    // 记录名称映射关系

                    txtLog.AppendText($"【{originalName}】 ==> {imageCount}图片、{videoCount}视频、{txtCount}文本文档\r\n");

                    // 重命名文件夹
                    if (checkBox1.Checked)
                    {
                        try
                        {
                            Directory.Move(subDir, Path.Combine(Path.GetDirectoryName(subDir), newName));
                        }
                        catch (Exception ex)
                        {
                            txtLog.SelectionColor = Color.Red;
                            txtLog.AppendText($"重命名出错: {ex.Message}\r\n");
                        }
                        txtLog.SelectionColor = Color.Brown;
                        txtLog.AppendText($"【{originalName}】已重命名为【{newName}】 \r\n");
                    }

                }
                txtLog.SelectionFont = new Font(txtLog.Font, FontStyle.Bold); // 设置为粗体
                txtLog.SelectionColor = Color.Green;
                txtLog.AppendText($"执行完成\r\n");
            }
        }


        private void CountImagesAndVideos(string folderPath, out int imageCount, out int videoCount, out int txtCount)
        {
            // 初始化计数器
            imageCount = 0;
            videoCount = 0;
            txtCount = 0;

            // 统计图片和视频数量
            foreach (string file in Directory.GetFiles(folderPath))
            {
                string extension = Path.GetExtension(file).ToLower();
                if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                {
                    imageCount++;
                }
                else if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                {
                    videoCount++;
                }
                else if (extension == ".txt")
                {
                    txtCount++;
                }
            }

            // 遍历子文件夹，统计图片和视频数量
            foreach (string subDir in Directory.GetDirectories(folderPath, "*", SearchOption.AllDirectories))
            {
                foreach (string file in Directory.GetFiles(subDir))
                {
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".png" || extension == ".jpeg")
                    {
                        imageCount++;
                    }
                    else if (extension == ".mp4" || extension == ".avi" || extension == ".mov")
                    {
                        videoCount++;
                    }
                    else if (extension == ".txt")
                    {
                        txtCount++;
                    }
                }
            }
        }

        private string GetFolderSizeString(string folderPath)
        {
            DirectoryInfo di = new DirectoryInfo(folderPath);
            long sizeBytes = 0;
            foreach (FileInfo fi in di.GetFiles("*", SearchOption.AllDirectories))
            {
                sizeBytes += fi.Length;
            }
            return GetFileSizeString(sizeBytes);
        }

        private string GetFileSizeString(long fileSizeBytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = fileSizeBytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len /= 1024;
            }

            string fileSizeString = String.Format("{0:0.##} {1}", len, sizes[order]);
            return fileSizeString;
        }
        private void txtFolderPath_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            explainBox.AppendText("程序开发：慕研\r\n");
            explainBox.Select(0, 8); // 设置选择范围，即要加粗的文本

            explainBox.SelectionFont = new Font(explainBox.Font.Name, 18, FontStyle.Bold);
            explainBox.SelectionAlignment = HorizontalAlignment.Center; // 将文本水平居中对齐

            // 设置第二行的文本
            explainBox.AppendText("本程序用于视频、图片、文本文档Txt计数，请将你需要批处理的文件夹“们”的上一级文件夹拖入程序中，程序会自动批量计数这些文件夹的视图数量以及容量大小，勾选命名会将原文件名换为：你文件夹名字 [nP-nV-nT=nKB/MB/GB/]");

        }


        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void explainBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void share_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
