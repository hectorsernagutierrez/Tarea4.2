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

namespace PersonahectorsOntology
{
	[ExcludeFromCodeCoverage]
	public class EpocaPath : GnossOCBase
	{
		public EpocaPath() : base() { } 

		public EpocaPath(SemanticEntityModel pSemCmsModel, LanguageEnum idiomaUsuario) : base()
		{
			mGNOSSID = pSemCmsModel.Entity.Uri;
			mURL = pSemCmsModel.Properties.FirstOrDefault(p => p.PropertyValues.Any(prop => prop.DownloadUrl != null))?.FirstPropertyValue.DownloadUrl;
			Try_epocaNode = new List<Concept>();
			SemanticPropertyModel propTry_epocaNode = pSemCmsModel.GetPropertyByPath("http://try.gnoss.com/epocaNode");
			if(propTry_epocaNode != null && propTry_epocaNode.PropertyValues.Count > 0)
			{
				foreach (SemanticPropertyModel.PropertyValue propValue in propTry_epocaNode.PropertyValues)
				{
					if(propValue.RelatedEntity!=null){
						Concept try_epocaNode = new Concept(propValue.RelatedEntity,idiomaUsuario);
						Try_epocaNode.Add(try_epocaNode);
					}
				}
			}
		}

		public virtual string RdfType { get { return "http://try.gnoss.com/EpocaPath"; } }
		public virtual string RdfsLabel { get { return "http://try.gnoss.com/EpocaPath"; } }
		public OntologyEntity Entity { get; set; }

		[LABEL(LanguageEnum.es,"")]
		[RDFProperty("http://try.gnoss.com/epocaNode")]
		public  List<Concept> Try_epocaNode { get; set;}
		public List<string> IdsTry_epocaNode { get; set;}


		internal override void GetProperties()
		{
			base.GetProperties();
			propList.Add(new ListStringOntologyProperty("try:epocaNode", this.IdsTry_epocaNode));
		}

		internal override void GetEntities()
		{
			base.GetEntities();
		} 











	}
}
