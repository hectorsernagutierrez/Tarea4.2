
using Gnoss.ApiWrapper.ApiModel;
using Gnoss.ApiWrapper;
using System.Xml;
using Gnoss.ApiWrapper.Model;
using GenerohectorsOntology;
using PeliculahectorsOntology;
using System.Reflection.Metadata;
using PersonahectorsOntology;

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

foreach (var guidUsuario in mCommunityApi.GetCommunityInfo().users)
{
    Console.WriteLine(guidUsuario.ToString());
    //KeyValuePair<Guid, Userlite> primerUsuario = mUserApi.GetUsersByIds(new List<Guid>() { guidUsuario }).FirstOrDefault();
    //string nombrePrimerUsuario = primerUsuario.Value.user_short_name;
}
#region TesauroComunidad
mCommunityApi.Log.Debug("Inicio de la Carga del tesauro de la comunidad");
mCommunityApi.Log.Debug("**************************************");

// Si hay recursos Categorizados contra alguna categoría del Tesauro, no se puede borrar el TESAURO
// Eliminamos el vinculo de cualquier recurso con cualquier Categoría
//BorrarCategoriasDeRecursos("peliculacrudapi");
//BorrarCategoriasDeRecursos("personacrudapi");
string xml2 = mCommunityApi.GetThesaurus();
Console.WriteLine(xml2);
// Leemos del XML la estrucutra del Tesauro (Categorías) a cargar en la Comunidad
XmlDocument xmlCategorias = new XmlDocument();
xmlCategorias.Load($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Documents\\ESTRUCTURA_CATEGORIAS_COMPLETO_MOD_SIN.xml");
//mCommunityApi.CreateThesaurus(xmlCategorias.OuterXml);

//Obtenemos el Tesauro de Categorías de la comunidad (XML)
string xml = mCommunityApi.GetThesaurus();

//Imprimimos por Consola el Tesauro Cargado
Console.WriteLine(xml);

mCommunityApi.Log.Debug("**************************************");
mCommunityApi.Log.Debug("Fin de la Carga del tesauro de comunidad (categorías)'");

#endregion TesauroComunidad

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
try
{
    mResourceApi.LoadSecondaryResource(generoSR);
    if (!generoSR.Uploaded)
    {
        mResourceApi.Log.Error(mensajeFalloCarga);
    }
}
catch (Exception)
{
    mResourceApi.Log.Error($"Exception -> {mensajeFalloCarga}");
}
#endregion Carga de géneros


#region Carga de personas (PRINCIPAL)
{
    mResourceApi.ChangeOntology("personahectors.owl");
    Person personActor1 = new Person();
    personActor1.Schema_name.Add(GnossBase.GnossOCBase.LanguageEnum.es, "Persona Prueba");
    // personActor1.Schema_name = "Actor1";
    //personActor1.Try_birthPlace = buscarUbicacionPorNombre("España", mResourceApi);
    /*
        Guid guid1 = new Guid("");
        Guid guid2 = new Guid("");
        ComplexOntologyResource resorceLoad = personActor1.ToGnossApiResource(mResourceApi, null, guid1, guid2);
    */
    ComplexOntologyResource resorceLoad = personActor1.ToGnossApiResource(mResourceApi, new List<string>() { "Categoria1" }, Guid.NewGuid(), Guid.NewGuid());
    mResourceApi.LoadComplexSemanticResource(resorceLoad);
}

#endregion Carga de personas (PRINCIPAL)

