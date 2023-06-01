// Decompiled with JetBrains decompiler
// Type: System.Drawing.Design.ToolboxItem
// Assembly: System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a
// MVID: 1AF41839-925B-4409-B823-837D7B5F29D6
// Assembly location: C:\Windows\Microsoft.NET\assembly\GAC_MSIL\System.Drawing\v4.0_4.0.0.0__b03f5f7f11d50a3a\System.Drawing.dll
// XML documentation location: C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.8\System.Drawing.xml

using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Windows.Forms;

namespace System.Drawing.Design
{
  /// <summary>Provides a base implementation of a toolbox item.</summary>
  [Serializable]
  [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
  [PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
  public class ToolboxItem : ISerializable
  {
    private static TraceSwitch ToolboxItemPersist = new TraceSwitch("ToolboxPersisting", "ToolboxItem: write data");
    private static object EventComponentsCreated = new object();
    private static object EventComponentsCreating = new object();
    private static bool isScalingInitialized = false;
    private const int ICON_DIMENSION = 16;
    private static int iconWidth = 16;
    private static int iconHeight = 16;
    private bool locked;
    private ToolboxItem.LockableDictionary properties;
    private ToolboxComponentsCreatedEventHandler componentsCreatedEvent;
    private ToolboxComponentsCreatingEventHandler componentsCreatingEvent;

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class.</summary>
    public ToolboxItem()
    {
      if (ToolboxItem.isScalingInitialized)
        return;
      if (DpiHelper.IsScalingRequired)
      {
        ToolboxItem.iconWidth = DpiHelper.LogicalToDeviceUnitsX(16);
        ToolboxItem.iconHeight = DpiHelper.LogicalToDeviceUnitsY(16);
      }
      ToolboxItem.isScalingInitialized = true;
    }

    /// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItem" /> class that creates the specified type of component.</summary>
    /// <param name="toolType">The type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates.</param>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
    public ToolboxItem(Type toolType)
      : this()
    {
      this.Initialize(toolType);
    }

    private ToolboxItem(SerializationInfo info, StreamingContext context)
      : this()
    {
      this.Deserialize(info, context);
    }

    /// <summary>Gets or sets the name of the assembly that contains the type or types that the toolbox item creates.</summary>
    /// <returns>An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly containing the type or types to create.</returns>
    public AssemblyName AssemblyName
    {
      get => (AssemblyName) this.Properties[(object) nameof (AssemblyName)];
      set => this.Properties[(object) nameof (AssemblyName)] = (object) value;
    }

    /// <summary>Gets or sets the <see cref="T:System.Reflection.AssemblyName" /> for the toolbox item.</summary>
    /// <returns>An array of <see cref="T:System.Reflection.AssemblyName" /> objects.</returns>
    public AssemblyName[] DependentAssemblies
    {
      get
      {
        AssemblyName[] property = (AssemblyName[]) this.Properties[(object) nameof (DependentAssemblies)];
        return property != null ? (AssemblyName[]) property.Clone() : (AssemblyName[]) null;
      }
      set => this.Properties[(object) nameof (DependentAssemblies)] = value.Clone();
    }

    /// <summary>Gets or sets a bitmap to represent the toolbox item in the toolbox.</summary>
    /// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
    public Bitmap Bitmap
    {
      get => (Bitmap) this.Properties[(object) nameof (Bitmap)];
      set => this.Properties[(object) nameof (Bitmap)] = (object) value;
    }

    /// <summary>Gets or sets the original bitmap that will be used in the toolbox for this item.</summary>
    /// <returns>A <see cref="T:System.Drawing.Bitmap" /> that represents the toolbox item in the toolbox.</returns>
    public Bitmap OriginalBitmap
    {
      get => (Bitmap) this.Properties[(object) nameof (OriginalBitmap)];
      set => this.Properties[(object) nameof (OriginalBitmap)] = (object) value;
    }

    /// <summary>Gets or sets the company name for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that specifies the company for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public string Company
    {
      get => (string) this.Properties[(object) nameof (Company)];
      set => this.Properties[(object) nameof (Company)] = (object) value;
    }

    /// <summary>Gets the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that specifies the component type for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public virtual string ComponentType => System.Drawing.SR.GetString("DotNET_ComponentType");

    /// <summary>Gets or sets the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that specifies the description for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public string Description
    {
      get => (string) this.Properties[(object) nameof (Description)];
      set => this.Properties[(object) nameof (Description)] = (object) value;
    }

    /// <summary>Gets or sets the display name for the toolbox item.</summary>
    /// <returns>The display name for the toolbox item.</returns>
    public string DisplayName
    {
      get => (string) this.Properties[(object) nameof (DisplayName)];
      set => this.Properties[(object) nameof (DisplayName)] = (object) value;
    }

    /// <summary>Gets or sets the filter that determines whether the toolbox item can be used on a destination component.</summary>
    /// <returns>An <see cref="T:System.Collections.ICollection" /> of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects.</returns>
    public ICollection Filter
    {
      get => (ICollection) this.Properties[(object) nameof (Filter)];
      set => this.Properties[(object) nameof (Filter)] = (object) value;
    }

    /// <summary>Gets a value indicating whether the toolbox item is transient.</summary>
    /// <returns>
    /// <see langword="true" />, if this toolbox item should not be stored in any toolbox database when an application that is providing a toolbox closes; otherwise, <see langword="false" />.</returns>
    public bool IsTransient
    {
      get => (bool) this.Properties[(object) nameof (IsTransient)];
      set => this.Properties[(object) nameof (IsTransient)] = (object) value;
    }

    /// <summary>Gets a value indicating whether the <see cref="T:System.Drawing.Design.ToolboxItem" /> is currently locked.</summary>
    /// <returns>
    /// <see langword="true" /> if the toolbox item is locked; otherwise, <see langword="false" />.</returns>
    public virtual bool Locked => this.locked;

    /// <summary>Gets a dictionary of properties.</summary>
    /// <returns>A dictionary of name/value pairs (the names are property names and the values are property values).</returns>
    public IDictionary Properties
    {
      get
      {
        if (this.properties == null)
          this.properties = new ToolboxItem.LockableDictionary(this, 8);
        return (IDictionary) this.properties;
      }
    }

    /// <summary>Gets or sets the fully qualified name of the type of <see cref="T:System.ComponentModel.IComponent" /> that the toolbox item creates when invoked.</summary>
    /// <returns>The fully qualified type name of the type of component that this toolbox item creates.</returns>
    public string TypeName
    {
      get => (string) this.Properties[(object) nameof (TypeName)];
      set => this.Properties[(object) nameof (TypeName)] = (object) value;
    }

    /// <summary>Gets the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that specifies the version for this <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public virtual string Version => this.AssemblyName != null ? this.AssemblyName.Version.ToString() : string.Empty;

    /// <summary>Occurs immediately after components are created.</summary>
    public event ToolboxComponentsCreatedEventHandler ComponentsCreated
    {
      add => this.componentsCreatedEvent += value;
      remove => this.componentsCreatedEvent -= value;
    }

    /// <summary>Occurs when components are about to be created.</summary>
    public event ToolboxComponentsCreatingEventHandler ComponentsCreating
    {
      add => this.componentsCreatingEvent += value;
      remove => this.componentsCreatingEvent -= value;
    }

    /// <summary>Throws an exception if the toolbox item is currently locked.</summary>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> is locked.</exception>
    protected void CheckUnlocked()
    {
      if (this.Locked)
        throw new InvalidOperationException(System.Drawing.SR.GetString("ToolboxItemLocked"));
    }

    /// <summary>Creates the components that the toolbox item is configured to create.</summary>
    /// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
    public IComponent[] CreateComponents() => this.CreateComponents((IDesignerHost) null);

    /// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
    /// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
    public IComponent[] CreateComponents(IDesignerHost host)
    {
      this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
      IComponent[] componentsCore = this.CreateComponentsCore(host, (IDictionary) new Hashtable());
      if (componentsCore != null && componentsCore.Length != 0)
        this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(componentsCore));
      return componentsCore;
    }

    /// <summary>Creates the components that the toolbox item is configured to create, using the specified designer host and default values.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to use when creating the components.</param>
    /// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
    /// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
    public IComponent[] CreateComponents(IDesignerHost host, IDictionary defaultValues)
    {
      this.OnComponentsCreating(new ToolboxComponentsCreatingEventArgs(host));
      IComponent[] componentsCore = this.CreateComponentsCore(host, defaultValues);
      if (componentsCore != null && componentsCore.Length != 0)
        this.OnComponentsCreated(new ToolboxComponentsCreatedEventArgs(componentsCore));
      return componentsCore;
    }

    /// <summary>Creates a component or an array of components when the toolbox item is invoked.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> to host the toolbox item.</param>
    /// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
    protected virtual IComponent[] CreateComponentsCore(IDesignerHost host)
    {
      ArrayList arrayList = new ArrayList();
      Type type = this.GetType(host, this.AssemblyName, this.TypeName, true);
      if (type != (Type) null)
      {
        if (host != null)
          arrayList.Add((object) host.CreateComponent(type));
        else if (typeof (IComponent).IsAssignableFrom(type))
          arrayList.Add(TypeDescriptor.CreateInstance((IServiceProvider) null, type, (Type[]) null, (object[]) null));
      }
      IComponent[] componentsCore = new IComponent[arrayList.Count];
      arrayList.CopyTo((Array) componentsCore, 0);
      return componentsCore;
    }

    /// <summary>Creates an array of components when the toolbox item is invoked.</summary>
    /// <param name="host">The designer host to use when creating components.</param>
    /// <param name="defaultValues">A dictionary of property name/value pairs of default values with which to initialize the component.</param>
    /// <returns>An array of created <see cref="T:System.ComponentModel.IComponent" /> objects.</returns>
    protected virtual IComponent[] CreateComponentsCore(
      IDesignerHost host,
      IDictionary defaultValues)
    {
      IComponent[] componentsCore = this.CreateComponentsCore(host);
      if (host != null)
      {
        for (int index1 = 0; index1 < componentsCore.Length; ++index1)
        {
          if (host.GetDesigner(componentsCore[index1]) is IComponentInitializer designer)
          {
            bool flag = true;
            try
            {
              designer.InitializeNewComponent(defaultValues);
              flag = false;
            }
            finally
            {
              if (flag)
              {
                for (int index2 = 0; index2 < componentsCore.Length; ++index2)
                  host.DestroyComponent(componentsCore[index2]);
              }
            }
          }
        }
      }
      return componentsCore;
    }

    /// <summary>Loads the state of the toolbox item from the specified serialization information object.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to load from.</param>
    /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
    protected virtual void Deserialize(SerializationInfo info, StreamingContext context)
    {
      string[] strArray = (string[]) null;
      foreach (SerializationEntry serializationEntry in info)
      {
        if (serializationEntry.Name.Equals("PropertyNames"))
        {
          strArray = serializationEntry.Value as string[];
          break;
        }
      }
      if (strArray == null)
        strArray = new string[6]
        {
          "AssemblyName",
          "Bitmap",
          "DisplayName",
          "Filter",
          "IsTransient",
          "TypeName"
        };
      foreach (SerializationEntry serializationEntry in info)
      {
        foreach (string str in strArray)
        {
          if (str.Equals(serializationEntry.Name))
          {
            this.Properties[(object) serializationEntry.Name] = serializationEntry.Value;
            break;
          }
        }
      }
      if (!info.GetBoolean("Locked"))
        return;
      this.Lock();
    }

    private static bool AreAssemblyNamesEqual(AssemblyName name1, AssemblyName name2)
    {
      if (name1 == name2)
        return true;
      return name1 != null && name2 != null && name1.FullName == name2.FullName;
    }

    /// <summary>Determines whether two <see cref="T:System.Drawing.Design.ToolboxItem" /> instances are equal.</summary>
    /// <param name="obj">The <see cref="T:System.Drawing.Design.ToolboxItem" /> to compare with the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</param>
    /// <returns>
    /// <see langword="true" /> if the specified <see cref="T:System.Drawing.Design.ToolboxItem" /> is equal to the current <see cref="T:System.Drawing.Design.ToolboxItem" />; otherwise, <see langword="false" />.</returns>
    public override bool Equals(object obj)
    {
      if (this == obj)
        return true;
      if (obj == null || !(obj.GetType() == this.GetType()))
        return false;
      ToolboxItem toolboxItem = (ToolboxItem) obj;
      return this.TypeName == toolboxItem.TypeName && ToolboxItem.AreAssemblyNamesEqual(this.AssemblyName, toolboxItem.AssemblyName) && this.DisplayName == toolboxItem.DisplayName;
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A hash code for the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public override int GetHashCode()
    {
      string typeName = this.TypeName;
      return (typeName != null ? typeName.GetHashCode() : 0) ^ this.DisplayName.GetHashCode();
    }

    /// <summary>Filters a property value before returning it.</summary>
    /// <param name="propertyName">The name of the property to filter.</param>
    /// <param name="value">The value against which to filter the property.</param>
    /// <returns>A filtered property value.</returns>
    protected virtual object FilterPropertyValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case "AssemblyName":
          if (value != null)
          {
            value = ((AssemblyName) value).Clone();
            break;
          }
          break;
        case "DisplayName":
        case "TypeName":
          if (value == null)
          {
            value = (object) string.Empty;
            break;
          }
          break;
        case "Filter":
          if (value == null)
          {
            value = (object) new ToolboxItemFilterAttribute[0];
            break;
          }
          break;
        case "IsTransient":
          if (value == null)
          {
            value = (object) false;
            break;
          }
          break;
      }
      return value;
    }

    /// <summary>Enables access to the type associated with the toolbox item.</summary>
    /// <param name="host">The designer host to query for <see cref="T:System.ComponentModel.Design.ITypeResolutionService" />.</param>
    /// <returns>The type associated with the toolbox item.</returns>
    public Type GetType(IDesignerHost host) => this.GetType(host, this.AssemblyName, this.TypeName, false);

    /// <summary>Creates an instance of the specified type, optionally using a specified designer host and assembly name.</summary>
    /// <param name="host">The <see cref="T:System.ComponentModel.Design.IDesignerHost" /> for the current document. This can be <see langword="null" />.</param>
    /// <param name="assemblyName">An <see cref="T:System.Reflection.AssemblyName" /> that indicates the assembly that contains the type to load. This can be <see langword="null" />.</param>
    /// <param name="typeName">The name of the type to create an instance of.</param>
    /// <param name="reference">A value indicating whether or not to add a reference to the assembly that contains the specified type to the designer host's set of references.</param>
    /// <returns>An instance of the specified type, if it can be located.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="typeName" /> is not specified.</exception>
    protected virtual Type GetType(
      IDesignerHost host,
      AssemblyName assemblyName,
      string typeName,
      bool reference)
    {
      ITypeResolutionService resolutionService = (ITypeResolutionService) null;
      Type type = (Type) null;
      if (typeName == null)
        throw new ArgumentNullException(nameof (typeName));
      if (host != null)
        resolutionService = (ITypeResolutionService) host.GetService(typeof (ITypeResolutionService));
      if (resolutionService != null)
      {
        if (reference)
        {
          if (assemblyName != null)
          {
            resolutionService.ReferenceAssembly(assemblyName);
            type = resolutionService.GetType(typeName);
          }
          else
          {
            type = resolutionService.GetType(typeName);
            if (type == (Type) null)
              type = Type.GetType(typeName);
            if (type != (Type) null)
              resolutionService.ReferenceAssembly(type.Assembly.GetName());
          }
        }
        else
        {
          if (assemblyName != null)
          {
            Assembly assembly = resolutionService.GetAssembly(assemblyName);
            if (assembly != (Assembly) null)
              type = assembly.GetType(typeName);
          }
          if (type == (Type) null)
            type = resolutionService.GetType(typeName);
        }
      }
      else if (!string.IsNullOrEmpty(typeName))
      {
        if (assemblyName != null)
        {
          Assembly assembly = (Assembly) null;
          try
          {
            assembly = Assembly.Load(assemblyName);
          }
          catch (FileNotFoundException ex)
          {
          }
          catch (BadImageFormatException ex)
          {
          }
          catch (IOException ex)
          {
          }
          if (assembly == (Assembly) null && assemblyName.CodeBase != null)
          {
            if (assemblyName.CodeBase.Length > 0)
            {
              try
              {
                assembly = Assembly.LoadFrom(assemblyName.CodeBase);
              }
              catch (FileNotFoundException ex)
              {
              }
              catch (BadImageFormatException ex)
              {
              }
              catch (IOException ex)
              {
              }
            }
          }
          if (assembly != (Assembly) null)
            type = assembly.GetType(typeName);
        }
        if (type == (Type) null)
          type = Type.GetType(typeName, false);
      }
      return type;
    }

    private AssemblyName GetNonRetargetedAssemblyName(Type type, AssemblyName policiedAssemblyName)
    {
      if (type == (Type) null || policiedAssemblyName == null)
        return (AssemblyName) null;
      if (type.Assembly.FullName == policiedAssemblyName.FullName)
        return policiedAssemblyName;
      foreach (AssemblyName referencedAssembly in type.Assembly.GetReferencedAssemblies())
      {
        if (referencedAssembly.FullName == policiedAssemblyName.FullName)
          return referencedAssembly;
      }
      foreach (AssemblyName referencedAssembly in type.Assembly.GetReferencedAssemblies())
      {
        if (referencedAssembly.Name == policiedAssemblyName.Name)
          return referencedAssembly;
      }
      foreach (AssemblyName referencedAssembly in type.Assembly.GetReferencedAssemblies())
      {
        try
        {
          Assembly assembly = Assembly.Load(referencedAssembly);
          if (assembly != (Assembly) null)
          {
            if (assembly.FullName == policiedAssemblyName.FullName)
              return referencedAssembly;
          }
        }
        catch
        {
        }
      }
      return (AssemblyName) null;
    }

    /// <summary>Initializes the current toolbox item with the specified type to create.</summary>
    /// <param name="type">The <see cref="T:System.Type" /> that the toolbox item creates.</param>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Drawing.Design.ToolboxItem" /> was locked.</exception>
    public virtual void Initialize(Type type)
    {
      this.CheckUnlocked();
      if (!(type != (Type) null))
        return;
      this.TypeName = type.FullName;
      AssemblyName name1 = type.Assembly.GetName(true);
      if (type.Assembly.GlobalAssemblyCache)
        name1.CodeBase = (string) null;
      Dictionary<string, AssemblyName> dictionary = new Dictionary<string, AssemblyName>();
      for (Type type1 = type; type1 != (Type) null; type1 = type1.BaseType)
      {
        AssemblyName name2 = type1.Assembly.GetName(true);
        AssemblyName retargetedAssemblyName = this.GetNonRetargetedAssemblyName(type, name2);
        if (retargetedAssemblyName != null && !dictionary.ContainsKey(retargetedAssemblyName.FullName))
          dictionary[retargetedAssemblyName.FullName] = retargetedAssemblyName;
      }
      AssemblyName[] assemblyNameArray = new AssemblyName[dictionary.Count];
      int num = 0;
      foreach (AssemblyName assemblyName in dictionary.Values)
        assemblyNameArray[num++] = assemblyName;
      this.DependentAssemblies = assemblyNameArray;
      this.AssemblyName = name1;
      this.DisplayName = type.Name;
      if (type.Assembly.ReflectionOnly)
        return;
      object[] customAttributes = type.Assembly.GetCustomAttributes(typeof (AssemblyCompanyAttribute), true);
      if (customAttributes != null && customAttributes.Length != 0 && customAttributes[0] is AssemblyCompanyAttribute companyAttribute && companyAttribute.Company != null)
        this.Company = companyAttribute.Company;
      DescriptionAttribute attribute1 = (DescriptionAttribute) TypeDescriptor.GetAttributes(type)[typeof (DescriptionAttribute)];
      if (attribute1 != null)
        this.Description = attribute1.Description;
      ToolboxBitmapAttribute attribute2 = (ToolboxBitmapAttribute) TypeDescriptor.GetAttributes(type)[typeof (ToolboxBitmapAttribute)];
      if (attribute2 != null)
      {
        if (attribute2.GetImage(type, false) is Bitmap original)
        {
          this.OriginalBitmap = attribute2.GetOriginalBitmap();
          if (original.Width != ToolboxItem.iconWidth || original.Height != ToolboxItem.iconHeight)
            original = new Bitmap((Image) original, new Size(ToolboxItem.iconWidth, ToolboxItem.iconHeight));
        }
        this.Bitmap = original;
      }
      bool flag = false;
      ArrayList arrayList = new ArrayList();
      foreach (Attribute attribute3 in TypeDescriptor.GetAttributes(type))
      {
        if (attribute3 is ToolboxItemFilterAttribute itemFilterAttribute)
        {
          if (itemFilterAttribute.FilterString.Equals(this.TypeName))
            flag = true;
          arrayList.Add((object) itemFilterAttribute);
        }
      }
      if (!flag)
        arrayList.Add((object) new ToolboxItemFilterAttribute(this.TypeName));
      this.Filter = (ICollection) arrayList.ToArray(typeof (ToolboxItemFilterAttribute));
    }

    /// <summary>Locks the toolbox item and prevents changes to its properties.</summary>
    public virtual void Lock() => this.locked = true;

    /// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreated" /> event.</summary>
    /// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatedEventArgs" /> that provides data for the event.</param>
    protected virtual void OnComponentsCreated(ToolboxComponentsCreatedEventArgs args)
    {
      if (this.componentsCreatedEvent == null)
        return;
      this.componentsCreatedEvent((object) this, args);
    }

    /// <summary>Raises the <see cref="E:System.Drawing.Design.ToolboxItem.ComponentsCreating" /> event.</summary>
    /// <param name="args">A <see cref="T:System.Drawing.Design.ToolboxComponentsCreatingEventArgs" /> that provides data for the event.</param>
    protected virtual void OnComponentsCreating(ToolboxComponentsCreatingEventArgs args)
    {
      if (this.componentsCreatingEvent == null)
        return;
      this.componentsCreatingEvent((object) this, args);
    }

    /// <summary>Saves the state of the toolbox item to the specified serialization information object.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to save to.</param>
    /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that indicates the stream characteristics.</param>
    protected virtual void Serialize(SerializationInfo info, StreamingContext context)
    {
      int num = ToolboxItem.ToolboxItemPersist.TraceVerbose ? 1 : 0;
      info.AddValue("Locked", this.Locked);
      ArrayList arrayList = new ArrayList(this.Properties.Count);
      foreach (DictionaryEntry property in this.Properties)
      {
        arrayList.Add(property.Key);
        info.AddValue((string) property.Key, property.Value);
      }
      info.AddValue("PropertyNames", (object) (string[]) arrayList.ToArray(typeof (string)));
    }

    /// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
    /// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Drawing.Design.ToolboxItem" />.</returns>
    public override string ToString() => this.DisplayName;

    /// <summary>Validates that an object is of a given type.</summary>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="value">Optional value against which to validate.</param>
    /// <param name="expectedType">The expected type of the property.</param>
    /// <param name="allowNull">
    /// <see langword="true" /> to allow <see langword="null" />; otherwise, <see langword="false" />.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is <see langword="null" />, and <paramref name="allowNull" /> is <see langword="false" />.</exception>
    /// <exception cref="T:System.ArgumentException">
    /// <paramref name="value" /> is not the type specified by <paramref name="expectedType" />.</exception>
    protected void ValidatePropertyType(
      string propertyName,
      object value,
      Type expectedType,
      bool allowNull)
    {
      if (value == null)
      {
        if (!allowNull)
          throw new ArgumentNullException(nameof (value));
      }
      else if (!expectedType.IsInstanceOfType(value))
        throw new ArgumentException(System.Drawing.SR.GetString("ToolboxItemInvalidPropertyType", (object) propertyName, (object) expectedType.FullName), nameof (value));
    }

    /// <summary>Validates a property before it is assigned to the property dictionary.</summary>
    /// <param name="propertyName">The name of the property to validate.</param>
    /// <param name="value">The value against which to validate.</param>
    /// <returns>The value used to perform validation.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// <paramref name="value" /> is <see langword="null" />, and <paramref name="propertyName" /> is "IsTransient".</exception>
    protected virtual object ValidatePropertyValue(string propertyName, object value)
    {
      switch (propertyName)
      {
        case "AssemblyName":
          this.ValidatePropertyType(propertyName, value, typeof (AssemblyName), true);
          break;
        case "Bitmap":
          this.ValidatePropertyType(propertyName, value, typeof (Bitmap), true);
          break;
        case "Company":
        case "Description":
        case "DisplayName":
        case "TypeName":
          this.ValidatePropertyType(propertyName, value, typeof (string), true);
          if (value == null)
          {
            value = (object) string.Empty;
            break;
          }
          break;
        case "Filter":
          this.ValidatePropertyType(propertyName, value, typeof (ICollection), true);
          int length = 0;
          ICollection collection = (ICollection) value;
          if (collection != null)
          {
            foreach (object obj in (IEnumerable) collection)
            {
              if (obj is ToolboxItemFilterAttribute)
                ++length;
            }
          }
          ToolboxItemFilterAttribute[] itemFilterAttributeArray = new ToolboxItemFilterAttribute[length];
          if (collection != null)
          {
            int num = 0;
            foreach (object obj in (IEnumerable) collection)
            {
              if (obj is ToolboxItemFilterAttribute itemFilterAttribute)
                itemFilterAttributeArray[num++] = itemFilterAttribute;
            }
          }
          value = (object) itemFilterAttributeArray;
          break;
        case "IsTransient":
          this.ValidatePropertyType(propertyName, value, typeof (bool), false);
          break;
        case "OriginalBitmap":
          this.ValidatePropertyType(propertyName, value, typeof (Bitmap), true);
          break;
      }
      return value;
    }

    /// <summary>For a description of this member, see the <see cref="M:System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
    /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data.</param>
    /// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization.</param>
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
      System.Drawing.IntSecurity.UnmanagedCode.Demand();
      this.Serialize(info, context);
    }

    private class LockableDictionary : Hashtable
    {
      private ToolboxItem _item;

      internal LockableDictionary(ToolboxItem item, int capacity)
        : base(capacity)
      {
        this._item = item;
      }

      public override bool IsFixedSize => this._item.Locked;

      public override bool IsReadOnly => this._item.Locked;

      public override object this[object key]
      {
        get
        {
          string propertyName = this.GetPropertyName(key);
          object obj = base[(object) propertyName];
          return this._item.FilterPropertyValue(propertyName, obj);
        }
        set
        {
          string propertyName = this.GetPropertyName(key);
          value = this._item.ValidatePropertyValue(propertyName, value);
          this.CheckSerializable(value);
          this._item.CheckUnlocked();
          base[(object) propertyName] = value;
        }
      }

      public override void Add(object key, object value)
      {
        string propertyName = this.GetPropertyName(key);
        value = this._item.ValidatePropertyValue(propertyName, value);
        this.CheckSerializable(value);
        this._item.CheckUnlocked();
        base.Add((object) propertyName, value);
      }

      private void CheckSerializable(object value)
      {
        if (value != null && !value.GetType().IsSerializable)
          throw new ArgumentException(System.Drawing.SR.GetString("ToolboxItemValueNotSerializable", (object) value.GetType().FullName));
      }

      public override void Clear()
      {
        this._item.CheckUnlocked();
        base.Clear();
      }

      private string GetPropertyName(object key)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        if (!(key is string propertyName) || propertyName.Length == 0)
          throw new ArgumentException(System.Drawing.SR.GetString("ToolboxItemInvalidKey"), nameof (key));
        return propertyName;
      }

      public override void Remove(object key)
      {
        this._item.CheckUnlocked();
        base.Remove(key);
      }
    }
  }
}
