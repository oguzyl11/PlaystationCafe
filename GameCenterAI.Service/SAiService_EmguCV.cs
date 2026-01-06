// This file contains the full EmguCV implementation
// After installing EmguCV NuGet packages, copy the code from this file to SAiService.cs
// and uncomment the EmguCV using statements

/*
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

// In SAiService class, replace the fields:
private Emgu.CV.Capture _camera;
private Emgu.CV.CascadeClassifier _faceClassifier;

// Replace CaptureAndDetectFace method with:
public byte[] CaptureAndDetectFace()
{
    try
    {
        // Initialize camera if not already initialized
        if (_camera == null)
        {
            _camera = new Emgu.CV.Capture(0);
        }

        // Load face cascade classifier
        if (_faceClassifier == null)
        {
            string cascadePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "haarcascade_frontalface_default.xml");
            
            if (!File.Exists(cascadePath))
            {
                cascadePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "GameCenterAI", "haarcascade_frontalface_default.xml");
                
                if (!File.Exists(cascadePath))
                {
                    throw new FileNotFoundException("haarcascade_frontalface_default.xml dosyası bulunamadı. Lütfen dosyayı uygulama klasörüne veya AppData\\GameCenterAI klasörüne kopyalayın.");
                }
            }

            _faceClassifier = new Emgu.CV.CascadeClassifier(cascadePath);
        }

        // Capture frame from camera
        Emgu.CV.Mat frame = _camera.QueryFrame();
        if (frame == null || frame.IsEmpty)
        {
            return null;
        }

        // Convert to grayscale for face detection
        Emgu.CV.Mat grayFrame = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(frame, grayFrame, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

        // Detect faces
        System.Drawing.Rectangle[] faces = _faceClassifier.DetectMultiScale(
            grayFrame,
            1.1,
            10,
            new System.Drawing.Size(20, 20));

        if (faces.Length == 0)
        {
            grayFrame.Dispose();
            return null;
        }

        // Get the largest face
        System.Drawing.Rectangle largestFace = faces.OrderByDescending(f => f.Width * f.Height).First();

        // Extract face region
        Emgu.CV.Mat faceRegion = new Emgu.CV.Mat(grayFrame, largestFace);

        // Convert to byte array
        byte[] faceBytes = null;
        using (Bitmap faceBitmap = faceRegion.ToBitmap())
        {
            using (MemoryStream ms = new MemoryStream())
            {
                faceBitmap.Save(ms, ImageFormat.Jpeg);
                faceBytes = ms.ToArray();
            }
        }

        grayFrame.Dispose();
        faceRegion.Dispose();

        return faceBytes;
    }
    catch (Exception ex)
    {
        throw new Exception("Yüz yakalama işlemi sırasında hata oluştu: " + ex.Message);
    }
}

// Replace CompareFaces method with:
public string CompareFaces(byte[] storedFace, byte[] currentFace, out bool eslesme)
{
    string hata = null;
    eslesme = false;
    
    if (storedFace == null || currentFace == null || storedFace.Length == 0 || currentFace.Length == 0)
    {
        return hata; // eslesme zaten false
    }

    try
    {
        // Convert byte arrays to Bitmap first, then to EmguCV Mat
        Bitmap storedBitmap;
        Bitmap currentBitmap;

        using (MemoryStream ms1 = new MemoryStream(storedFace))
        {
            storedBitmap = new Bitmap(ms1);
        }

        using (MemoryStream ms2 = new MemoryStream(currentFace))
        {
            currentBitmap = new Bitmap(ms2);
        }

        // Convert to EmguCV Mat
        Emgu.CV.Mat storedMat = storedBitmap.ToMat();
        Emgu.CV.Mat currentMat = currentBitmap.ToMat();

        // Resize images to same size for comparison
        Emgu.CV.Mat resizedStored = new Emgu.CV.Mat();
        Emgu.CV.Mat resizedCurrent = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.Resize(storedMat, resizedStored, new System.Drawing.Size(100, 100));
        Emgu.CV.CvInvoke.Resize(currentMat, resizedCurrent, new System.Drawing.Size(100, 100));

        // Convert to grayscale
        Emgu.CV.Mat grayStored = new Emgu.CV.Mat();
        Emgu.CV.Mat grayCurrent = new Emgu.CV.Mat();
        Emgu.CV.CvInvoke.CvtColor(resizedStored, grayStored, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);
        Emgu.CV.CvInvoke.CvtColor(resizedCurrent, grayCurrent, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

        // Calculate histogram comparison
        double similarity = Emgu.CV.CvInvoke.CompareHist(
            Emgu.CV.CvInvoke.CalcHist(new Emgu.CV.Mat[] { grayStored }, new int[] { 0 }, new Emgu.CV.Mat(), new int[] { 256 }, new float[] { 0, 256 }, false),
            Emgu.CV.CvInvoke.CalcHist(new Emgu.CV.Mat[] { grayCurrent }, new int[] { 0 }, new Emgu.CV.Mat(), new int[] { 256 }, new float[] { 0, 256 }, false),
            Emgu.CV.CvEnum.HistogramCompMethod.Correl);

        // Cleanup
        storedMat.Dispose();
        currentMat.Dispose();
        resizedStored.Dispose();
        resizedCurrent.Dispose();
        grayStored.Dispose();
        grayCurrent.Dispose();
        storedBitmap.Dispose();
        currentBitmap.Dispose();

        // Threshold for match (0.7 = 70% similarity)
        eslesme = similarity > 0.7;
    }
    catch (Exception ex)
    {
        hata = "Yüz karşılaştırma işlemi sırasında hata oluştu: " + ex.Message;
    }

    return hata;
}

// Replace Dispose method with:
public void Dispose()
{
    if (_camera != null)
    {
        _camera.Dispose();
        _camera = null;
    }

    if (_faceClassifier != null)
    {
        _faceClassifier.Dispose();
        _faceClassifier = null;
    }
}
*/

