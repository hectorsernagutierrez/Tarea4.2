using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Gnoss.ApiWrapper;
using Gnoss.ApiWrapper.Model;
using Gnoss.ApiWrapper.Helpers;
using GnossBase;
using Es.Riam.Gnoss.Web.MVC.Models;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections;
using Gnoss.ApiWrapper.Exceptions;
using System.Diagnostics.CodeAnalysis;
using Movie = PeliculahectorsOntology.Movie;

namespace PersonahectorsOntology
{
	[ExcludeFromCodeCoverage]
	public class Person : GnossOCBase
	{
		public Person() : base() { } 

		public Person(SemanticResourceModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			GNOSSID = pSemCmsModel.RootEntities[0].Entity.Uri;
			SemanticPropertyModel propSchema_birthDate = pSemCmsModel.GetPropertyByPath("http://schema.org/birthDate");
			if (propSchema_birthDate != null && propSchema_birthDate.PropertyValues.Count > 0 && propSchema_birthDate.PropertyValues[0].RelatedEntity != null)
			{
				Schema_birthDate = new EpocaPath(propSchema_birthDate.PropertyValues[0].RelatedEntity,idiomaUsuario);
			}
			Try_authorOf = new List<Movie>();
			SemanticPropertyModel propTry_authorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/authorOf");
			if(propTry_authorOf != null && propTry_authorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_authorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_authorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_authorOf.Add(try_authorOf);
					}
				}
			}
			Try_actorOf = new List<Movie>();
			SemanticPropertyModel propTry_actorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/actorOf");
			if(propTry_actorOf != null && propTry_actorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_actorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_actorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_actorOf.Add(try_actorOf);
					}
				}
			}
			Try_directorOf = new List<Movie>();
			SemanticPropertyModel propTry_directorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/directorOf");
			if(propTry_directorOf != null && propTry_directorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_directorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_directorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_directorOf.Add(try_directorOf);
					}
				}
			}
			this.Schema_name = new Dictionary<LanguageEnum,string>();
			this.Schema_name.Add(idiomaUsuario , GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name")));
			
		}

		public Person(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			mGNOSSID = pSemCmsModel.Entity.Uri;
			mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			SemanticPropertyModel propSchema_birthDate = pSemCmsModel.GetPropertyByPath("http://schema.org/birthDate");
			if (propSchema_birthDate != null && propSchema_birthDate.PropertyValues.Count > 0 && propSchema_birthDate.PropertyValues[0].RelatedEntity != null)
			{
				Schema_birthDate = new EpocaPath(propSchema_birthDate.PropertyValues[0].RelatedEntity,idiomaUsuario);
			}
			Try_authorOf = new List<Movie>();
			SemanticPropertyModel propTry_authorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/authorOf");
			if(propTry_authorOf != null && propTry_authorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_authorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_authorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_authorOf.Add(try_authorOf);
					}
				}
			}
			Try_actorOf = new List<Movie>();
			SemanticPropertyModel propTry_actorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/actorOf");
			if(propTry_actorOf != null && propTry_actorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_actorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_actorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_actorOf.Add(try_actorOf);
					}
				}
			}
			Try_directorOf = new List<Movie>();
			SemanticPropertyModel propTry_directorOf = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/directorOf");
			if(propTry_directorOf != null && propTry_directorOf.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_directorOf.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Movie try_directorOf = new Movie(propValue.RelatedEntity,idiomaUsuario);
						Try_directorOf.Add(try_directorOf);
					}
				}
			}
			this.Schema_name = new Dictionary<LanguageEnum,string>();
			this.Schema_name.Add(idiomaUsuario , GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://schema.org/name")));
			
		}

		public virtual string RdfType { get { return "http://schema.org/Person"; } }
		public virtual string RdfsLabel { get { return "http://schema.org/Person"; } }
		[LABEL(LanguageEnum.en,"Date of birth")]
		[LABEL(LanguageEnum.es,"Fecha de Nacimiento")]
		[RDFProperty("http://schema.org/birthDate")]
		public  EpocaPath Schema_birthDate { get; set;}

		[LABEL(LanguageEnum.en,"Author of:")]
		[LABEL(LanguageEnum.es,"Autor de:")]
		[RDFProperty("http://try.gnoss.com/authorOf")]
		public  List<Movie> Try_authorOf { get; set;}
		public List<string> IdsTry_authorOf { get; set;}

		[LABEL(LanguageEnum.en,"Actor of:")]
		[LABEL(LanguageEnum.es,"Actor de:")]
		[RDFProperty("http://try.gnoss.com/actorOf")]
		public  List<Movie> Try_actorOf { get; set;}
		public List<string> IdsTry_actorOf { get; set;}

		[LABEL(LanguageEnum.en,"Director of:")]
		[LABEL(LanguageEnum.es,"Director de:")]
		[RDFProperty("http://try.gnoss.com/directorOf")]
		public  List<Movie> Try_directorOf { get; set;}
		public List<string> IdsTry_directorOf { get; set;}

		[RDFProperty("http://schema.org/image")]
		public  List<string> Schema_image { get; set;}

		[LABEL(LanguageEnum.en,"Name of the person")]
		[LABEL(LanguageEnum.es,"Nombre de la persona")]
		[RDFProperty("http://schema.org/name")]
		public  Dictionary<LanguageEnum,string> Schema_name { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new ListStringOntologyProperty("try:authorOf", this.IdsTry_authorOf));
			propList.Add(new ListStringOntologyProperty("try:actorOf", this.IdsTry_actorOf));
			propList.Add(new ListStringOntologyProperty("try:directorOf", this.IdsTry_directorOf));
			propList.Add(new ListStringOntologyProperty("schema:image", this.Schema_image));
			if(this.Schema_name != null)
			{
				foreach (LanguageEnum idioma in this.Schema_name.Keys)
				{
					propList.Add(new StringOntologyProperty("schema:name", this.Schema_name[idioma], idioma.ToString()));
				}
			}
			else
			{
				throw new GnossAPIException($"La propiedad schema:name debe tener al menos un valor en el recurso: {resourceID}");
			}
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			if(Schema_birthDate!=null){
				Schema_birthDate.GetProperties();
				Schema_birthDate.GetEntities();
				OntologyEntity entitySchema_birthDate = new OntologyEntity("http://try.gnoss.com/EpocaPath", "http://try.gnoss.com/EpocaPath", "schema:birthDate", Schema_birthDate.propList, Schema_birthDate.entList);
				Schema_birthDate.Entity = entitySchema_birthDate;
				entList.Add(entitySchema_birthDate);
			}
		} 
		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI)
		{
			return ToGnossApiResource(resourceAPI, new List<string>());
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, listaDeCategorias, Guid.Empty, Guid.Empty);
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<Guid> listaDeCategorias)
		{
			return ToGnossApiResource(resourceAPI, null, Guid.Empty, Guid.Empty, listaDeCategorias);
		}

		public virtual ComplexOntologyResource ToGnossApiResource(ResourceApi resourceAPI, List<string> listaDeCategorias, Guid idrecurso, Guid idarticulo, List<Guid> listaIdDeCategorias = null)
		{
			ComplexOntologyResource resource = new ComplexOntologyResource();
			Ontology ontology = null;
			GetEntities();
			GetProperties();
			if(idrecurso.Equals(Guid.Empty) && idarticulo.Equals(Guid.Empty))
			{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, RdfType, RdfsLabel, prefList, propList, entList);
			}
			else{
				ontology = new Ontology(resourceAPI.GraphsUrl, resourceAPI.OntologyUrl, RdfType, RdfsLabel, prefList, propList, entList,idrecurso,idarticulo);
			}
			resource.Id = GNOSSID;
			resource.Ontology = ontology;
			resource.TextCategories = listaDeCategorias;
			resource.CategoriesIds = listaIdDeCategorias;
			AddResourceTitle(resource);
			AddResourceDescription(resource);
			AddImages(resource);
			AddFiles(resource);
			return resource;
		}

		public override List<string> ToOntologyGnossTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"<http://schema.org/Person>", list, " . ");
			AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://www.w3.org/2000/01/rdf-schema#label", $"\"http://schema.org/Person\"", list, " . ");
			AgregarTripleALista($"{resourceAPI.GraphsUrl}{ResourceID}", "http://gnoss/hasEntidad", $"<{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}>", list, " . ");
			if(this.Schema_birthDate != null)
			{
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"<http://try.gnoss.com/EpocaPath>", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}", "http://www.w3.org/2000/01/rdf-schema#label", $"\"http://try.gnoss.com/EpocaPath\"", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}{ResourceID}", "http://gnoss/hasEntidad", $"<{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}>", list, " . ");
				AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://schema.org/birthDate", $"<{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}>", list, " . ");
				if(this.Schema_birthDate.IdsTry_epocaNode != null)
				{
					foreach(var item2 in this.Schema_birthDate.IdsTry_epocaNode)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}", "http://try.gnoss.com/epocaNode", $"<{item2}>", list, " . ");
					}
				}
			}
				if(this.IdsTry_authorOf != null)
				{
					foreach(var item2 in this.IdsTry_authorOf)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://try.gnoss.com/authorOf", $"<{item2}>", list, " . ");
					}
				}
				if(this.IdsTry_actorOf != null)
				{
					foreach(var item2 in this.IdsTry_actorOf)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://try.gnoss.com/actorOf", $"<{item2}>", list, " . ");
					}
				}
				if(this.IdsTry_directorOf != null)
				{
					foreach(var item2 in this.IdsTry_directorOf)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://try.gnoss.com/directorOf", $"<{item2}>", list, " . ");
					}
				}
				if(this.Schema_image != null)
				{
					foreach(var item2 in this.Schema_image)
					{
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://schema.org/image", $"<{item2}>", list, " . ");
					}
				}
				if(this.Schema_name != null)
				{
							foreach (LanguageEnum idioma in this.Schema_name.Keys)
							{
								AgregarTripleALista($"{resourceAPI.GraphsUrl}items/Person_{ResourceID}_{ArticleID}", "http://schema.org/name",  $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name[idioma])}\"", list,  $"@{idioma} . ");
							}
				}
			return list;
		}

		public override List<string> ToSearchGraphTriples(ResourceApi resourceAPI)
		{
			List<string> list = new List<string>();
			List<string> listaSearch = new List<string>();
			AgregarTags(list);
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://www.w3.org/1999/02/22-rdf-syntax-ns#type", $"\"personahectors\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/type", $"\"http://schema.org/Person\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasfechapublicacion", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hastipodoc", "\"5\"", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasfechamodificacion", $"{DateTime.Now.ToString("yyyyMMddHHmmss")}", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasnumeroVisitas", "0", list, " . ");
			AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasprivacidadCom", "\"publico\"", list, " . ");
			foreach(LanguageEnum idioma in this.Schema_name.Keys)
			{
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://xmlns.com/foaf/0.1/firstName", $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name[idioma])}\"", list, $"@{idioma} . ");
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasnombrecompleto", $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name[idioma])}\"", list, $"@{idioma} . ");
			}
			string search = string.Empty;
			if(this.Schema_birthDate != null)
			{
				AgregarTripleALista($"http://gnossAuxiliar/{ResourceID.ToString().ToUpper()}", "http://gnoss/hasEntidadAuxiliar", $"<{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}>", list, " . ");
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://schema.org/birthDate", $"<{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}>", list, " . ");
				if(this.Schema_birthDate.IdsTry_epocaNode != null)
				{
					foreach(var item2 in this.Schema_birthDate.IdsTry_epocaNode)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"{resourceAPI.GraphsUrl}items/EpocaPath_{ResourceID}_{this.Schema_birthDate.ArticleID}", "http://try.gnoss.com/epocaNode", $"<{itemRegex}>", list, " . ");
					}
				}
			}
				if(this.IdsTry_authorOf != null)
				{
					foreach(var item2 in this.IdsTry_authorOf)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://try.gnoss.com/authorOf", $"<{itemRegex}>", list, " . ");
					}
				}
				if(this.IdsTry_actorOf != null)
				{
					foreach(var item2 in this.IdsTry_actorOf)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://try.gnoss.com/actorOf", $"<{itemRegex}>", list, " . ");
					}
				}
				if(this.IdsTry_directorOf != null)
				{
					foreach(var item2 in this.IdsTry_directorOf)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://try.gnoss.com/directorOf", $"<{itemRegex}>", list, " . ");
					}
				}
				if(this.Schema_image != null)
				{
					foreach(var item2 in this.Schema_image)
					{
					Regex regex = new Regex(@"\/items\/.+_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}_[0-9A-Fa-f]{8}[-]?(?:[0-9A-Fa-f]{4}[-]?){3}[0-9A-Fa-f]{12}");
					string itemRegex = item2;
					if (regex.IsMatch(itemRegex))
					{
						itemRegex = $"http://gnoss/{resourceAPI.GetShortGuid(itemRegex).ToString().ToUpper()}";
					}
					else
					{
						itemRegex = itemRegex.ToLower();
					}
						AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://schema.org/image", $"<{itemRegex}>", list, " . ");
					}
				}
				if(this.Schema_name != null)
				{
							foreach (LanguageEnum idioma in this.Schema_name.Keys)
							{
								AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://schema.org/name",  $"\"{GenerarTextoSinSaltoDeLinea(this.Schema_name[idioma])}\"", list,  $"@{idioma} . ");
							}
				}
			if (listaSearch != null && listaSearch.Count > 0)
			{
				foreach(string valorSearch in listaSearch)
				{
					search += $"{valorSearch} ";
				}
			}
			if(!string.IsNullOrEmpty(search))
			{
				AgregarTripleALista($"http://gnoss/{ResourceID.ToString().ToUpper()}", "http://gnoss/search", $"\"{GenerarTextoSinSaltoDeLinea(search.ToLower())}\"", list, " . ");
			}
			return list;
		}

		public override KeyValuePair<Guid, string> ToAcidData(ResourceApi resourceAPI)
		{

			//Insert en la tabla Documento
			string tags = "";
			foreach(string tag in tagList)
			{
				tags += $"{tag}, ";
			}
			if (!string.IsNullOrEmpty(tags))
			{
				tags = tags.Substring(0, tags.LastIndexOf(','));
			}
			string titulo = string.Empty;
			foreach(LanguageEnum idioma in this.Schema_name.Keys)
			{
				titulo += $"{this.Schema_name[idioma]}@{idioma}||| ";
			}
			titulo = $"{titulo.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\"", "\"\"").Replace("'", "#COMILLA#").Replace("|", "#PIPE#")}";
			string descripcion = string.Empty;
			if(this.Schema_name != null) {
				foreach(LanguageEnum idioma in this.Schema_name.Keys)
				{
					descripcion += $"{this.Schema_name[idioma]}@{idioma}||| ";
				}
			}
			descripcion = $"{descripcion.Replace("\r\n", "").Replace("\n", "").Replace("\r", "").Replace("\"", "\"\"").Replace("'", "#COMILLA#").Replace("|", "#PIPE#")}";
			string tablaDoc = $"'{titulo}', '{descripcion}', '{resourceAPI.GraphsUrl}', '{tags}'";
			KeyValuePair<Guid, string> valor = new KeyValuePair<Guid, string>(ResourceID, tablaDoc);

			return valor;
		}

		public override string GetURI(ResourceApi resourceAPI)
		{
			return $"{resourceAPI.GraphsUrl}items/PersonahectorsOntology_{ResourceID}_{ArticleID}";
		}


		internal void AddResourceTitle(ComplexOntologyResource resource)
		{
			List<Multilanguage> multiTitleList = new List<Multilanguage>();
			foreach (LanguageEnum idioma in this.Schema_name.Keys)
			{
				multiTitleList.Add(new Multilanguage(this.Schema_name[idioma], idioma.ToString()));
			}
			resource.MultilanguageTitle = multiTitleList;
		}

		internal void AddResourceDescription(ComplexOntologyResource resource)
		{
			List<Multilanguage> listMultilanguageDescription = new List<Multilanguage>();
			foreach (LanguageEnum idioma in this.Schema_name.Keys)
			{
				listMultilanguageDescription.Add(new Multilanguage(this.Schema_name[idioma], idioma.ToString()));
			}
			resource.MultilanguageDescription = listMultilanguageDescription;
		}




	}
}
