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
using Concept = TesauropersonahectorsOntology.Concept;

namespace TesauropersonahectorsOntology
{
	[ExcludeFromCodeCoverage]
	public class Concept : GnossOCBase
	{
		public Concept(string pIdentificador) : base()
		{
			Identificador = pIdentificador;
		}

		public Concept(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			mGNOSSID = pSemCmsModel.Entity.Uri;
			mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			Skos_related = new List<Concept>();
			SemanticPropertyModel propSkos_related = pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#related");
			if(propSkos_related != null && propSkos_related.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSkos_related.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Concept skos_related = new Concept(propValue.RelatedEntity,idiomaUsuario);
						Skos_related.Add(skos_related);
					}
				}
			}
			Skos_broader = new List<Concept>();
			SemanticPropertyModel propSkos_broader = pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#broader");
			if(propSkos_broader != null && propSkos_broader.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSkos_broader.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Concept skos_broader = new Concept(propValue.RelatedEntity,idiomaUsuario);
						Skos_broader.Add(skos_broader);
					}
				}
			}
			Skos_narrower = new List<Concept>();
			SemanticPropertyModel propSkos_narrower = pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#narrower");
			if(propSkos_narrower != null && propSkos_narrower.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propSkos_narrower.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Concept skos_narrower = new Concept(propValue.RelatedEntity,idiomaUsuario);
						Skos_narrower.Add(skos_narrower);
					}
				}
			}
			this.Skos_symbol = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#symbol"));
			this.Skos_note = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#note"));
			this.Dc_identifier = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://purl.org/dc/elements/1.1/identifier"));
			this.Skos_prefLabel = GetPropertyValueSemCms(pSemCmsModel.GetPropertyByPath("http://www.w3.org/2008/05/skos#prefLabel"));
var item0 = GetDateValuePropertySemCms(pSemCmsModel.GetPropertyByPath("http://purl.org/dc/elements/1.1/source"));
if(item0.HasValue){
			this.Dc_source = item0.Value;
}
		}

		public virtual string RdfType { get { return "http://www.w3.org/2008/05/skos#Concept"; } }
		public virtual string RdfsLabel { get { return "http://www.w3.org/2008/05/skos#Concept"; } }
		public string Identificador { get; set; }
		public OntologyEntity Entity { get; set; }

		[LABEL(LanguageEnum.es,"Nodos relacionados")]
		[RDFProperty("http://www.w3.org/2008/05/skos#related")]
		public  List<Concept> Skos_related { get; set;}

		[LABEL(LanguageEnum.es,"Padres")]
		[RDFProperty("http://www.w3.org/2008/05/skos#broader")]
		public  List<Concept> Skos_broader { get; set;}

		[LABEL(LanguageEnum.es,"Hijos")]
		[RDFProperty("http://www.w3.org/2008/05/skos#narrower")]
		public  List<Concept> Skos_narrower { get; set;}

		[LABEL(LanguageEnum.es,"Símbolo")]
		[RDFProperty("http://www.w3.org/2008/05/skos#symbol")]
		public  string Skos_symbol { get; set;}

		[RDFProperty("http://www.w3.org/2008/05/skos#note")]
		public  string Skos_note { get; set;}

		[LABEL(LanguageEnum.es,"Identificador")]
		[RDFProperty("http://purl.org/dc/elements/1.1/identifier")]
		public  string Dc_identifier { get; set;}

		[LABEL(LanguageEnum.es,"Nombre de la categoria")]
		[RDFProperty("http://www.w3.org/2008/05/skos#prefLabel")]
		public  string Skos_prefLabel { get; set;}

		[LABEL(LanguageEnum.es,"Fuente")]
		[RDFProperty("http://purl.org/dc/elements/1.1/source")]
		public  DateTime Dc_source { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new StringOntologyProperty("skos:symbol", this.Skos_symbol));
			propList.Add(new StringOntologyProperty("skos:note", this.Skos_note));
			propList.Add(new StringOntologyProperty("dc:identifier", this.Dc_identifier));
			propList.Add(new StringOntologyProperty("skos:prefLabel", this.Skos_prefLabel));
			propList.Add(new DateOntologyProperty("dc:source", this.Dc_source));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
			if(Skos_related!=null){
				foreach(Concept prop in Skos_related){
					prop.GetProperties();
					prop.GetEntities();
					OntologyEntity entityConcept = new OntologyEntity("http://www.w3.org/2008/05/skos#Concept", "http://www.w3.org/2008/05/skos#Concept", "skos:related", prop.propList, prop.entList);
				entList.Add(entityConcept);
				prop.Entity = entityConcept;
				}
			}
			if(Skos_broader!=null){
				foreach(Concept prop in Skos_broader){
					prop.GetProperties();
					prop.GetEntities();
					OntologyEntity entityConcept = new OntologyEntity("http://www.w3.org/2008/05/skos#Concept", "http://www.w3.org/2008/05/skos#Concept", "skos:broader", prop.propList, prop.entList);
				entList.Add(entityConcept);
				prop.Entity = entityConcept;
				}
			}
			if(Skos_narrower!=null){
				foreach(Concept prop in Skos_narrower){
					prop.GetProperties();
					prop.GetEntities();
					OntologyEntity entityConcept = new OntologyEntity("http://www.w3.org/2008/05/skos#Concept", "http://www.w3.org/2008/05/skos#Concept", "skos:narrower", prop.propList, prop.entList);
				entList.Add(entityConcept);
				prop.Entity = entityConcept;
				}
			}
		} 



		public override KeyValuePair<Guid, string> ToAcidData(ResourceApi resourceAPI)
		{
			KeyValuePair<Guid, string> valor = new KeyValuePair<Guid, string>();

			return valor;
		}








	}
}
