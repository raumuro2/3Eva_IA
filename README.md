# Lector de Imágenes con IA
Este proyecto es una aplicación de escritorio desarrollada en C# utilizando WPF que permite a los usuarios cargar imágenes y obtener una descripción de las mismas utilizando servicios de IA de Azure.

![image](https://github.com/user-attachments/assets/0b1083a2-0360-404d-91b4-d7699533a8fb)

## Características
- Interfaz de usuario intuitiva para cargar imágenes.
- Análisis de imágenes utilizando Azure Computer Vision.
- Lectura en voz alta del texto detectado en las imágenes.
- Selección de diferentes voces para la lectura del texto.
- Configuración personalizable para el idioma y tipo de voz.

## Requisitos previos
1. Visual Studio 2019 o superior
2. .NET Framework 4.7.2
3. Una cuenta de Azure con los siguientes servicios configurados:
- Azure Computer Vision
- Azure Speech Services

## Configuración
1. Clone este repositorio en su máquina local.
2. Restaure los paquetes NuGet necesarios.
3. Al iniciar la aplicación se creará el archivo claves.json en la ruta que te aparezca por pantalla.
Reemplace los valores de este archivo con sus propias claves y endpoints de Azure.

## Uso
1. Ejecute la aplicación.
2. Haga clic en "Subir imagen" para seleccionar una imagen de su computadora.
3. Puede seleccionar diferentes voces para la lectura del texto utilizando el menú desplegable.
4. Una vez cargada la imagen, haga clic en el botón de flecha para analizarla.
5. La aplicación mostrará una descripción de la imagen y leerá en voz alta cualquier texto detectado.

## Estructura del proyecto
- MainWindow.xaml/.cs: Interfaz principal y lógica de la aplicación.
- AzureAPI.cs: Contiene los métodos para interactuar con los servicios de Azure.
- ClavesSingleton.cs: Maneja la carga segura de las claves de API.
- ConfiguracionWindow.xaml/.cs: Ventana para configurar opciones adicionales (en desarrollo).

## Licencia
Este proyecto está licenciado bajo la Licencia MIT. Ver [LICENCIA](LICENSE.txt) para detalles.
