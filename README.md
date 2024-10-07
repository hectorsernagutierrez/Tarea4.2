# Tarea4.2
# Proyecto Consola .NET CORE 6 - Gestión de Datos y Tesauro

## Primera Parte

### Básico
1. Crear una solución de consola en .NET CORE 6.
2. Referenciar en el proyecto principal el proyecto con las clases generadas automáticamente.
   - **Nota:** Un fallo al descargar las clases generadas automáticamente implica un error en la configuración de alguno de los OCs, por lo que será necesario revisar dicha configuración.
3. Actualizar GNOSS API Wrapper.
4. Compilar el proyecto y resolver los errores que puedan aparecer.
   - **Nota:** Los errores en las clases generadas automáticamente indican un fallo en la configuración de alguno de los OCs, lo que requerirá su revisión.
5. Agregar el archivo OAuth.
6. Referenciar el archivo OAuth dentro del código, ejecutar el proyecto y verificar en el log de la consola que la conexión con la comunidad se ha realizado correctamente.

### Intermedio
1. Crear un repositorio en GIT y GitHub para el proyecto.
2. Generar una rama de desarrollo y realizar un primer commit con el estado del proyecto actual.
3. Realizar una Pull Request (PR) contra la rama principal y aprobarla para incorporar los cambios.

## Segunda Parte

### Básico
1. Cargar un género de prueba.
2. Cargar una persona de prueba.
3. Obtener los datos de la persona de prueba y modificar su nombre.
4. Eliminar la persona de prueba.
5. Cargar una nueva persona de prueba.
6. Cargar una película de prueba, asignando como actor a la persona de prueba y como género al género de prueba.

### Intermedio
1. Planificar la carga de los datos contenidos en los archivos JSON del archivo `datospeliculas.zip`.
2. Programar la carga de los datos contenidos en los archivos JSON de `datospeliculas.zip`.

### Avanzado
1. Cargar el tesauro principal de la comunidad desde un archivo XML.
2. Cargar el tesauro semántico de un OC secundario.

## Configuración
En el archivo de configuración del proyecto (`config`), deben colocarse las credenciales de administrador de la comunidad. Es importante destacar que estas credenciales **no deben ser subidas al repositorio**.

## Uso de NuGet Packages
Este proyecto utiliza los siguientes paquetes de NuGet:
- **ApiGnossWrapper**: Para la interacción con la API de GNOSS.
- **Newtonsoft.Json**: Para la gestión de archivos JSON.

## Información de la Comunidad
La comunidad incluye más de 5000 películas, las cuales están disponibles para gestionar a través de las funcionalidades del proyecto.

## Documentación
Existe documentación detallada sobre la clase de persistencia utilizada en el proyecto, que explica su uso y configuración.

## Soporte
Cualquier error puede ser comunicado a **hectorserna@gnoss.com**.

## Última Modificación
Este documento ha sido actualizado por última vez el **7 de octubre de 2024**.
