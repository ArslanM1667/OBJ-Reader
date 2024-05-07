// Подключение пространства имен для работы с OpenGL
using SharpGL;
// Подключение стандартных пространств имен
using System;
using System.IO;
using System.Windows.Forms;
// Подключение пространств имен для работы с графическими ресурсами в SharpGL
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Primitives;
// Подключение пространства имен для отладочной информации
using System.Diagnostics;

namespace OBJ_reader_SharpGL
{
    public partial class Form1 : Form
    {
        private OpenGLControl openGLControl; // Контрол для отображения OpenGL
        private ObjModel objModel; // Модель OBJ

        // Конструктор формы
        public Form1()
        {
            InitializeComponent();
            InitializeOpenGLControl();
        }

        // Инициализация контрола OpenGL
        private void InitializeOpenGLControl()
        {
            // Показываем сообщение о инициализации
            MessageBox.Show("Проверка", "Инициализация");
            openGLControl = new OpenGLControl();
            openGLControl.Dock = DockStyle.Fill;
            openGLControl.OpenGLDraw += openGLControl1_OpenGLDraw;
            Controls.Add(openGLControl);
        }

        // Обработчик события отрисовки OpenGL контрола
        private void openGLControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            gl.LoadIdentity();

            // Настройка камеры и проекции здесь (зависит от вашей 3D модели)
            gl.Translate(0.0f, 0.0f, -5.0f);

            // Проверка, что objModel инициализирован и готов для отрисовки
            if (objModel != null)
            {
                objModel.PushObjectSpace(gl);
                objModel.Render(gl, SharpGL.SceneGraph.Core.RenderMode.Render);
                objModel.PopObjectSpace(gl);
            }

            openGLControl.Invalidate();
        }

        // Загрузка файла OBJ
        private void LoadObjFile(string filePath)
        {
            Debug.WriteLine($"Попытка загрузки файла по пути: {filePath}");

            if (File.Exists(filePath))
            {
                objModel = new ObjModel();
                objModel.Load(filePath);
                MessageBox.Show("Файл успешно загружен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Файл не найден!");
            }

            if (objModel != null)
            {
                // Вывести сообщение или залогировать, что модель загружена успешно
                MessageBox.Show("Модель OBJ успешно загружена.");
            }
            else
            {
                // Вывести сообщение об ошибке или залогировать, что модель не удалось загрузить
                MessageBox.Show("Не удалось загрузить модель OBJ.");
            }

        }

        // Обработчик события загрузки формы
        private void Form1_Load(object sender, EventArgs e)
        {
            // Путь к файлу .obj
            string filePath = "Models/apple/Apl_.obj";

            // Проверка существования файла и загрузка OBJ модели
            if (File.Exists(filePath))
            {
                MessageBox.Show("Файл найден.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadObjFile(filePath);
            }
            else
            {
                MessageBox.Show($"Файл по пути '{filePath}' не найден. Пожалуйста, убедитесь, что путь к файлу указан верно.", "Ошибка загрузки файла", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
}
