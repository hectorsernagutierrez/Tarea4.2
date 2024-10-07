/*
 * Autor:Hector Serna Gutierrez
 * Tarea 4.2
 * version 1.0
 * Ultima mod 07/10/2024
 * Contacto: hectorserna@gnos.com
 * Documento Program
 */
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper;
using System.Xml;
using Gnoss.ApiWrapper.Model;
using GenerohectorsOntology;
using PeliculahectorsOntology;
using System.Reflection.Metadata;
using PersonahectorsOntology;
using static GnossBase.GnossOCBase;
//using Tarea4._2.RepositorioPersistenciaJSON;
// Este program modifica ligeramente el código dado por la formación Gnoss para el manejo de la api adaptandolo a 
//Las necesidades del enunciado de la tarea 4.2 
//El nombre de cada region es autoexplicativo respecto a lo que hace.
// El código de la clase d ela capa de persitencia está documentado al no ser solamente meras modificaciones superficiales.

//Conexión a la comunidad
#region Conexion comunidad
string pathOAuth = @"Config\oAuth.config";
ResourceApi mResourceApi = new ResourceApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
CommunityApi mCommunityApi = new CommunityApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
ThesaurusApi mThesaurusApi = new ThesaurusApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
UserApi mUserApi = new UserApi(Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, pathOAuth));
Console.WriteLine($"Id de la Comunidad -> {mCommunityApi.GetCommunityId()}");
Console.WriteLine($"Nombre de la Comunidad -> {mCommunityApi.GetCommunityInfo().name}");
Console.WriteLine($"Nombre Corto de la Comunidad -> {mCommunityApi.GetCommunityInfo().short_name}");
Console.WriteLine($"Descripción de la comunidad inicial -> {mCommunityApi.GetCommunityInfo().description}");
Console.WriteLine($"Categorías de la Comunidad -> {string.Join(", ", mCommunityApi.CommunityCategories.Select(categoria => categoria.category_name))}");
Console.WriteLine("USUARIOS");
Dictionary<GnossBase.GnossOCBase.LanguageEnum, string> NamePersonDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();
Dictionary<GnossBase.GnossOCBase.LanguageEnum, string> NameMovieDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();
Dictionary<GnossBase.GnossOCBase.LanguageEnum, string> DescriptionMovieDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();
Dictionary<GnossBase.GnossOCBase.LanguageEnum, string> ImageMovieDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();
Dictionary<GnossBase.GnossOCBase.LanguageEnum, string> ContentRatingDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();



foreach (var guidUsuario in mCommunityApi.GetCommunityInfo().users)
{
    Console.WriteLine(guidUsuario.ToString());
    //KeyValuePair<Guid, Userlite> primerUsuario = mUserApi.GetUsersByIds(new List<Guid>() { guidUsuario }).FirstOrDefault();
    //string nombrePrimerUsuario = primerUsuario.Value.user_short_name;
}

#endregion Conexión comunidad
//#region TesauroComunidad
//mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de la comunidad");
//mCommunityApi.Log.Debug("**************************************");

//// Si hay recursos Categorizados contra alguna categoría del Tesauro, no se puede borrar el TESAURO
//// Eliminamos el vinculo de cualquier recurso con cualquier Categoría
////BorrarCategoriasDeRecursos("peliculacrudapi");
////BorrarCategoriasDeRecursos("personacrudapi");
//string xml2 = mCommunityApi.GetThesaurus();
//Console.WriteLine(xml2);
//// Leemos del XML la estrucutra del Tesauro (Categorías) a cargar en la Comunidad
//XmlDocument xmlCategorias = new XmlDocument();
//xmlCategorias.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\ESTRUCTURA_CATEGORIAS_COMPLETO_MOD_SIN.xml");
////mCommunityApi.CreateThesaurus(xmlCategorias.OuterXml);

////Obtenemos el Tesauro de Categorías de la comunidad (XML)
//string xml = mCommunityApi.GetThesaurus();

////Imprimimos por Consola el Tesauro Cargado
//Console.WriteLine(xml);

//mCommunityApi.Log.Debug("**************************************");
//mCommunityApi.Log.Debug("Fin de la Carga del tesauro de comunidad (categorías)'");

//#endregion TesauroComunidad

#region Carga del tesauro de una comunidad desde Archivo XML

mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de la comunidad de cine/películas");
mCommunityApi.Log.Debug("**************************************");


// Leemos del XML la estructura del Tesauro (Categorías) de cine/películas desde el archivo "PeliculaThesaurus.xml"
XmlDocument xmlCategorias = new XmlDocument();
xmlCategorias.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\PeliculaThesaurus.xml");  

// Crear el tesauro de la comunidad usando las categorías del cine/películas
//mCommunityApi.CreateThesaurus(xmlCategorias.OuterXml);

// Obtenemos el Tesauro de Categorías de la comunidad (XML) para confirmación
string xml = mCommunityApi.GetThesaurus();

// Imprimimos por Consola el Tesauro Cargado
Console.WriteLine(xml);

// Log de finalización
mCommunityApi.Log.Debug("**************************************");
mCommunityApi.Log.Debug("Fin de la Carga del tesauro de comunidad de cine/películas");

#endregion Carga del tesauro de una comunidad desde Archivo XML





#region TesauroComunidadEpocas
mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de épocas por fecha de nacimiento");
mCommunityApi.Log.Debug("**************************************");

// Cargar el tesauro desde el archivo XML de épocas
XmlDocument xmlEpocas = new XmlDocument();
xmlEpocas.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\EpocaThesaurus.xml");

// Cargar el tesauro en la comunidad
//mCommunityApi.CreateThesaurus(xmlEpocas.OuterXml);



mCommunityApi.Log.Debug("**************************************");
mCommunityApi.Log.Debug("Fin de la Carga del tesauro de épocas");
#endregion TesauroComunidadEpocas




#region Carga de un tesauro semántico basado en siglos y años de nacimiento + Modificación y eliminación de categorías

mCommunityApi.Log.Debug("Inicio de la Carga del tesauro semántico de épocas");
mCommunityApi.Log.Debug("**************************************");

// Diccionario de épocas (siglos) y los años dentro de cada siglo
Dictionary<string, List<string>> d_siglo_anos = new();

// Agregamos el Siglo XIX con años clave para actores, directores y guionistas
d_siglo_anos.Add("Siglo XIX", new List<string>(){
    "1890", "1880", "1870", "1860", "1850"
});

// Agregamos el Siglo XX con los años faltantes
d_siglo_anos.Add("Siglo XX", new List<string>(){
    "1990", "1980", "1970", "1960", "1950", "1940", "1930", "1920", "1910", "1900"
});

// Siglo XXI también con los años importantes
d_siglo_anos.Add("Siglo XXI", new List<string>(){
    "2020", "2010", "2000"
});

Thesaurus tesauroEpocas = new Thesaurus();

mThesaurusApi.DeleteThesaurus("category", "tesauropersonahectors");

tesauroEpocas.Source = "epoca";  // El concepto general sigue siendo "época"
tesauroEpocas.Ontology = "tesauropersonahectors";
tesauroEpocas.CommunityShortName = "demo-gnoss-akademia-20-39";
tesauroEpocas.Collection = new Collection();
tesauroEpocas.Collection.Member = new List<Concept>();
tesauroEpocas.Collection.ScopeNote = new Dictionary<string, string>() { { "es", "Épocas y Años de actores, directores y guionistas" } };
tesauroEpocas.Collection.Subject = "http://testing.gnoss.com/items/epoca";

// Procesamos cada siglo y sus años asociados
foreach (var sigloTesauroElement in d_siglo_anos.Keys)
{
    string nombreSigloParaURL = sigloTesauroElement.Replace(" ", "-").ToLower();
    Concept sigloConcept = new Concept();
    sigloConcept.PrefLabel = new Dictionary<string, string>() { { "es", sigloTesauroElement } };
    sigloConcept.Symbol = "1";
    sigloConcept.Identifier = $"epoca_siglo-{nombreSigloParaURL}-id"; // Propiedad "identifier" asociada al siglo
    sigloConcept.Subject = $"epoca_siglo-{nombreSigloParaURL}-sj"; // Se forma la URI del siglo
    sigloConcept.Narrower = new List<Concept>();

    // Procesamos cada año dentro del siglo
    foreach (var anoTesauroElement in d_siglo_anos[sigloTesauroElement])
    {
        string nombreAnoParaURL = anoTesauroElement.Replace(" ", "-").ToLower();
        Concept anoConcept = new Concept();
        anoConcept.PrefLabel = new Dictionary<string, string>() { { "es", anoTesauroElement } };
        anoConcept.Symbol = "2";
        anoConcept.Identifier = $"epoca_ano-{nombreSigloParaURL}-{nombreAnoParaURL}-id";
        anoConcept.Subject = $"epoca_ano-{nombreSigloParaURL}-{nombreAnoParaURL}-sj";
        sigloConcept.Narrower.Add(anoConcept); // Se asocian los años al siglo correspondiente
    }

    // Añadimos el concepto de siglo al tesauro
    tesauroEpocas.Collection.Member.Add(sigloConcept);
}


mCommunityApi.Log.Debug("**************************************");
mCommunityApi.Log.Debug("Fin de la Carga del tesauro de Epocas");

#endregion Carga de un tesauro semántico de épocas y años de actores, directores y guionistas




#region Carga de géneros (SECUNDARIA)
//string identificador = Guid.NewGuid().ToString(); //Se pone en el grafo de ontología
//Genre genero = new(identificador + "IDdistinctorio"); //Se pone en el grafo de búsqueda
//genero.Schema_name = "NombreGeneroAhIDdistinctorio";
//mResourceApi.ChangeOntology("generohectors.owl");
//SecondaryResource generoSR = genero.ToGnossApiResource(mResourceApi, identificador); 
//mResourceApi.LoadSecondaryResource(generoSR);

string identificador = Guid.NewGuid().ToString(); //Se pone en el grafo de ontología
Genre genero = new(identificador); //Se pone en el grafo de búsqueda
genero.Schema_name = "Fantasía";
mResourceApi.ChangeOntology("generohectors.owl");
SecondaryResource generoSR = genero.ToGnossApiResource(mResourceApi, $"Genre_{identificador}");
string mensajeFalloCarga = $"Error en la carga del Género con identificador {identificador} -> Nombre: {genero.Schema_name}";
//try
//{
//    mResourceApi.LoadSecondaryResource(generoSR);
//    if (!generoSR.Uploaded)
//    {
//        mResourceApi.Log.Error(mensajeFalloCarga);
//    }
//}
//catch (Exception)
//{
//    mResourceApi.Log.Error($"Exception -> {mensajeFalloCarga}");
//}
#endregion Carga de géneros


#region Carga de personas (PRINCIPAL)
{
    mResourceApi.ChangeOntology("personahectors.owl");
    Person personActor1 = new Person();
   // Dictionary<GnossBase.GnossOCBase.LanguageEnum,string> NamePersonDictionary = new Dictionary<GnossBase.GnossOCBase.LanguageEnum, string>();
    
   NamePersonDictionary.Add(GnossBase.GnossOCBase.LanguageEnum.es, "Persona prueba");
    
    personActor1.Schema_name = NamePersonDictionary;

    
    
    // personActor1.Schema_name = "Actor1";
    //personActor1.Try_birthPlace = buscarUbicacionPorNombre("España", mResourceApi);
    /*
        Guid guid1 = new Guid("");
        Guid guid2 = new Guid("");
        ComplexOntologyResource resorceLoad = personActor1.ToGnossApiResource(mResourceApi, null, guid1, guid2);
    */
    ComplexOntologyResource resorceLoad = personActor1.ToGnossApiResource(mResourceApi, new List<string>() { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
    mResourceApi.LoadComplexSemanticResource(resorceLoad);
}

#endregion Carga de personas (PRINCIPAL)

#region Modificación de personas (PRINCIPAL)

string uri = "";

//Obtención del id de la persona cargada en la comunidad
string pOntology = "personahectors";
string select = string.Empty, where = string.Empty;
select += $@"SELECT *";
where += $@" WHERE {{ ";
where += $@"?s ?p ?o.";
where += $@"FILTER(?o LIKE 'Persona prueba')";
where += $@"}}";


SparqlObject resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
//Si está ya en el grafo, obtengo la URI
if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
{
    foreach (var item in resultadoQuery.results.bindings)
    {
        uri = item["s"].value;
    }
}

//Obtención de los dos IDs a través de la URI
string[] partes = uri.Split('/', '_');

string resourceId = partes[5];
string articleID = partes[6];

Person personaActor1Modificado = new Person();
NamePersonDictionary.Remove(GnossBase.GnossOCBase.LanguageEnum.es);
NamePersonDictionary.Add(GnossBase.GnossOCBase.LanguageEnum.es, "Persona PruebaM");
personaActor1Modificado.Schema_name = NamePersonDictionary;

mResourceApi.ModifyComplexOntologyResource(personaActor1Modificado.ToGnossApiResource(mResourceApi, new List<string>() { "Categoría 1" }, new Guid(resourceId), new Guid(articleID)), false, true);

#endregion Modificación de personas (PRINCIPAL)

#region Borrado de personas (PRINCIPAL)
 uri = "";

//Obtención del id de la persona cargada en la comunidad
 pOntology = "personahectors";
select = string.Empty; where = string.Empty;
select += $@"SELECT *";
where += $@" WHERE {{ ";
where += $@"?s ?p ?o.";
where += $@"FILTER(?o LIKE 'Persona PruebaM')";
where += $@"}}";


 resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
//Si está ya en el grafo, obtengo la URI
if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0 && resultadoQuery.results.bindings.FirstOrDefault()?.Keys.Count > 0)
{
    foreach (var item in resultadoQuery.results.bindings)
    {
        uri = item["s"].value;
    }
}

if (uri.Split('/', '_').Length >= 6) {
    string mensajeFalloBorradoRecPrincipal = $"Error en el borrado de la Persona {uri} -> Nombre: {personaActor1Modificado.Schema_name}";
    try
    {
        mResourceApi.ChangeOntology("personahectors.owl");
        mResourceApi.PersistentDelete(mResourceApi.GetShortGuid(uri), true, true);
    }
    catch (Exception)
    {
        mResourceApi.Log.Error($"Exception -> {mensajeFalloBorradoRecPrincipal}");
    }
}else
{
    mResourceApi.Log.Error($"Exception -> URI FORMATO INCORRECTO");
}

#endregion Borrado de personas (PRINCIPAL)

#region Carga de personas (PRINCIPAL)
{
    mResourceApi.ChangeOntology("personahectors.owl");
    Person personActor2 = new Person();
    NamePersonDictionary.Remove(GnossBase.GnossOCBase.LanguageEnum.es);
    NamePersonDictionary.Add(GnossBase.GnossOCBase.LanguageEnum.es, "Actor");
    personActor2.Schema_name = NamePersonDictionary;
    ComplexOntologyResource resorceLoad = personActor2.ToGnossApiResource(mResourceApi, new List<string>() { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
   // mResourceApi.LoadComplexSemanticResource(resorceLoad);
}
#endregion Carga de personas (PRINCIPAL)

#region Carga de película con actor

uri = "";

 pOntology = "personahectors";
 select = string.Empty;
 where = string.Empty;
select += $@"SELECT *";
where += $@" WHERE {{ ";
where += $@"?s ?p ?o.";
where += $@"FILTER(?o LIKE 'Actor')";
where += $@"}}";

 resultadoQuery = mResourceApi.VirtuosoQuery(select, where, pOntology);
//Si está ya en el grafo, obtengo la URI
if (resultadoQuery != null && resultadoQuery.results != null && resultadoQuery.results.bindings != null && resultadoQuery.results.bindings.Count > 0)
{
    foreach (var item in resultadoQuery.results.bindings)
    {
        uri = item["s"].value;
    }
}
//Obtención de los dos IDs a través de la URI
 partes = uri.Split('/', '_');



Movie pelicula = new Movie();
ImageMovieDictionary.Add(LanguageEnum.es, "https://walpaper.es/wallpaper/2015/11/wallpaper-gratis-de-un-espectacular-paisaje-en-color-azul-en-hd.jpg");
pelicula.Schema_image = ImageMovieDictionary;
string pathImg = @"Img\Di_Caprio.jpg";
NameMovieDictionary.Add(LanguageEnum.es, "PeliPrueba");
pelicula.Schema_name = NameMovieDictionary;
DescriptionMovieDictionary.Add(LanguageEnum.es, "Deescripcion prueba español");
pelicula.Schema_description = DescriptionMovieDictionary;
pelicula.Schema_duration = new List<int>() { 135 };
pelicula.IdsSchema_actor = new List<string>() { uri };
ContentRatingDictionary.Add(GnossBase.GnossOCBase.LanguageEnum.es, "Content rating prueba");
pelicula.Schema_contentRating = ContentRatingDictionary;
mResourceApi.ChangeOntology("peliculahectors.owl");
ComplexOntologyResource resorceToLoad = pelicula.ToGnossApiResource(mResourceApi, new List<string>() { "Categoría 1" }, Guid.NewGuid(), Guid.NewGuid());
//string idPeliculaCargada = mResourceApi.LoadComplexSemanticResource(resorceToLoad);

#endregion Carga de película con actor

#region CargaTODOSJSON


PeliculaPersistencia moviePersistence = new PeliculaPersistencia(mResourceApi);


string movieJsonDirectory = @"datospeliculas\";


moviePersistence.CargarPeliculasDesdeJson(movieJsonDirectory);




#endregion CargaTODOSJSON



