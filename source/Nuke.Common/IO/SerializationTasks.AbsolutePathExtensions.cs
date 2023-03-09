﻿// Copyright 2023 Maintainers of NUKE.// Distributed under the MIT License.// https://github.com/nuke-build/nuke/blob/master/LICENSEusing System;using System.Xml.Linq;using JetBrains.Annotations;using Newtonsoft.Json;using Newtonsoft.Json.Linq;using Nuke.Common.Tooling;using YamlDotNet.Serialization;#pragma warning disable CS0618namespace Nuke.Common.IO{    public static partial class SerializationTasks    {        /// <summary>        /// Serializes an object as JSON to a file.        /// </summary>        public static void WriteJson<T>(this AbsolutePath path, T obj, Configure<JsonSerializerSettings> configurator = null)        {            JsonSerializeToFile(obj, path, configurator);        }        /// <summary>        /// Deserializes an object as JSON from a file.        /// </summary>        [Pure]        public static T ReadJson<T>(this AbsolutePath path, Configure<JsonSerializerSettings> configurator = null)        {            return JsonDeserializeFromFile<T>(path, configurator);        }        /// <summary>        /// Deserializes a <see cref="JObject"/> as JSON from a file.        /// </summary>        [Pure]        public static JObject ReadJson(this AbsolutePath path, Configure<JsonSerializerSettings> configurator = null)        {            return JsonDeserializeFromFile<JObject>(path, configurator);        }        /// <summary>        /// Deserializes an object as JSON from a file, applies updates, and serializes it back to the file.        /// </summary>        public static void UpdateJson<T>(            this AbsolutePath path,            Action<T> update,            Configure<JsonSerializerSettings> configurator = null)        {            JsonUpdateFile(path, update, configurator);        }        /// <summary>        /// Deserializes a <see cref="JObject"/> from a file, applies updates, and serializes it back to the file.        /// </summary>        public static void UpdateJson(            this AbsolutePath path,            Action<JObject> update,            Configure<JsonSerializerSettings> configurator = null)        {            JsonUpdateFile(path, update, configurator);        }        /// <summary>        /// Serializes an object as YAML to a file.        /// </summary>        public static void WriteYaml<T>(this AbsolutePath path, T obj, Configure<SerializerBuilder> configurator = null)        {            YamlSerializeToFile(obj, path, configurator);        }        /// <summary>        /// Deserializes an object as JSON from a file.        /// </summary>        [Pure]        public static T ReadYaml<T>(this AbsolutePath path, Configure<DeserializerBuilder> configurator = null)        {            return YamlDeserializeFromFile<T>(path, configurator);        }        /// <summary>        /// Deserializes an object as YAML from a file, applies updates, and serializes it back to the file.        /// </summary>        public static void UpdateYaml<T>(            this AbsolutePath path,            Action<T> update,            Configure<DeserializerBuilder> deserializationConfigurator = null,            Configure<SerializerBuilder> serializationConfigurator = null)        {            YamlUpdateFile(path, update, deserializationConfigurator, serializationConfigurator);        }        /// <summary>        /// Serializes an <see cref="XDocument"/> to a file.        /// </summary>        public static void WriteXml(this AbsolutePath path, XDocument obj, SaveOptions saveOptions = SaveOptions.None)        {            obj.Save(path, saveOptions);        }        /// <summary>        /// Serializes an object as XML to a file.        /// </summary>        public static void WriteXml<T>(this AbsolutePath path, T obj, SaveOptions saveOptions = SaveOptions.None)        {            XmlSerializeToFile(obj, path, saveOptions);        }        /// <summary>        /// Deserializes an <see cref="XDocument"/> from a file.        /// </summary>        [Pure]        public static XDocument ReadXml(this AbsolutePath path, LoadOptions options = LoadOptions.PreserveWhitespace)        {            Assert.FileExists(path);            return XDocument.Load(path, options);        }        /// <summary>        /// Deserializes an object as XML from a file.        /// </summary>        [Pure]        public static T ReadXml<T>(this AbsolutePath path)        {            return XmlDeserializeFromFile<T>(path);        }        /// <summary>        /// Deserializes an object as XML from a file, applies updates, and serializes it back to the file.        /// </summary>        public static void UpdateXml<T>(            this AbsolutePath path,            Action<T> update,            LoadOptions loadOptions = LoadOptions.PreserveWhitespace,            SaveOptions saveOptions = SaveOptions.None)        {            XmlUpdateFile(path, update, loadOptions, saveOptions);        }        /// <summary>        /// Deserializes a <see cref="XDocument"/> from a file, applies updates, and serializes it back to the file.        /// </summary>        public static void UpdateXml(            this AbsolutePath path,            Action<XDocument> update,            LoadOptions loadOptions = LoadOptions.PreserveWhitespace,            SaveOptions saveOptions = SaveOptions.None)        {            path.WriteXml(path.ReadXml(loadOptions), saveOptions);        }    }}