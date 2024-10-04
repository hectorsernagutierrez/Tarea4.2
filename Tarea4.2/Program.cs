
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
//Cambio contra la rama principal