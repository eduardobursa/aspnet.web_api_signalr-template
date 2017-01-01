using Microsoft.AspNet.SignalR.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Template.WebApi.Resolver
{
	/// <summary>
	/// Define um contract resolver para formatação camel case de propriedades.
	/// </summary>
	/// <seealso cref="Newtonsoft.Json.Serialization.DefaultContractResolver" />
	public class CamelCasePropertyNamesContractResolver : DefaultContractResolver
	{
		/// <summary>
		/// Inicializa uma nova instância da classe <see cref="CamelCasePropertyNamesContractResolver"/>.
		/// </summary>
		public CamelCasePropertyNamesContractResolver()
		{
			AssembliesToInclude = new HashSet<Assembly>();
			TypesToInclude = new HashSet<Type>();
		}

		/// <summary>
		/// Identifica os assemblies que devem ser incluíods para a serialização.
		/// </summary>
		public HashSet<Assembly> AssembliesToInclude { get; set; }

		/// <summary>
		/// Identifica os tipos que devem ser incluídos para serialização camel case.
		/// </summary>
		public HashSet<Type> TypesToInclude { get; set; }

		/// <summary>
		/// Creates a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
		/// </summary>
		/// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
		/// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
		/// <returns>
		/// A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.
		/// </returns>
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			Type declaringType = member.DeclaringType;
			var jsonProperty = base.CreateProperty(member, memberSerialization);

			if (TypesToInclude.Contains(declaringType) || AssembliesToInclude.Contains(declaringType.Assembly))
			{
				jsonProperty.PropertyName = JsonUtility.CamelCase(jsonProperty.PropertyName);
			}

			return jsonProperty;
		}
	}
}
