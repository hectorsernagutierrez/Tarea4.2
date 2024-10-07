
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


//#region TesauroComunidad
//mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de la comunidad");
//mCommunityApi.Log.Debug("**************************************");

//// Cargar el tesauro desde el nuevo archivo XML
//XmlDocument xmlCategoriasPeli = new XmlDocument();
//xmlCategorias.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\PeliculaThesaurus.xml");

//// Cargar el tesauro en la comunidad
//mCommunityApi.CreateThesaurus(xmlCategoriasPeli.OuterXml);

//// Obtener el Tesauro de Categorías de la comunidad (XML)
// xml = mCommunityApi.GetThesaurus();

//// Imprimir el Tesauro Cargado en consola
//Console.WriteLine(xml);

//mCommunityApi.Log.Debug("**************************************");
//mCommunityApi.Log.Debug("Fin de la Carga del tesauro de comunidad (categorías)'");
//#endregion TesauroComunidad



//#region TesauroComunidadEpocas
//mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de épocas por fecha de nacimiento");
//mCommunityApi.Log.Debug("**************************************");

//// Cargar el tesauro desde el archivo XML de épocas
//XmlDocument xmlEpocas = new XmlDocument();
//xmlEpocas.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\EpocaThesaurus.xml");

//// Cargar el tesauro en la comunidad
//mCommunityApi.CreateThesaurus(xmlEpocas.OuterXml);

//// Obtener el Tesauro de Categorías de la comunidad (XML)
// xml = mCommunityApi.GetThesaurus();

//// Imprimir el Tesauro Cargado en consola
//Console.WriteLine(xml);

//mCommunityApi.Log.Debug("**************************************");
//mCommunityApi.Log.Debug("Fin de la Carga del tesauro de épocas");
//#endregion TesauroComunidadEpocas




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



