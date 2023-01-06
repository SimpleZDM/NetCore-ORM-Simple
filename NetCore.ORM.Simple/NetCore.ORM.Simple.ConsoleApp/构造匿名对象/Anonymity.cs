using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetCore.ORM.Simple.ConsoleApp.构造匿名对象
{
    public class Anonymity
    {
        public object GetType()
        {
            string[] namelist = new string[] { "UserName", "UserId" };
            Dictionary<string, Type> dic = new Dictionary<string, Type>();
            dic.Add("UserName",typeof(string));
            dic.Add("UserId", typeof(int));
            string strDynamicNameModel = "jksdynamic";
            string strDynamiClassName = "<>jksdynamic";
            AppDomain currentDomain= AppDomain.CurrentDomain;
            AssemblyName assemblyName = new AssemblyName();
            assemblyName.Name = strDynamicNameModel;

            AssemblyBuilder  assemblyBuilder= AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.RunAndCollect);

            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(strDynamicNameModel);

            TypeBuilder typeBuilder = moduleBuilder.DefineType(strDynamiClassName,TypeAttributes.Public);

            Type[] methodArgs = { typeof(int) };

            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public, CallingConventions.Standard,null);

            int index = 0;
            ILGenerator ilg = constructorBuilder.GetILGenerator();

            foreach (var item in dic)
            {
                FieldBuilder fieldBuilder = typeBuilder.
                    DefineField($"{item.Value.Name}_{item.Key}",
                    item.Value,FieldAttributes.Private);
                index++;
                ilg.Emit(OpCodes.Ldarg_0);
                ilg.Emit(OpCodes.Ldarg,index);
                ilg.Emit(OpCodes.Stfld,fieldBuilder);
                ilg.Emit(OpCodes.Ret);

                //MethodBuilder methodBuilder = typeBuilder.DefineMethod($"get_{item.Key}", MethodAttributes.Public, item.Value, null);

                //PropertyBuilder propertyBuild = typeBuilder.DefineProperty(item.Key, PropertyAttributes.None, item.Value, null);

                //ILGenerator iLGenerator = methodBuilder.GetILGenerator();
                //iLGenerator.Emit(OpCodes.Ldarg_0);
                //iLGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
                //iLGenerator.Emit(OpCodes.Ret);
                //propertyBuild.SetGetMethod(methodBuilder);

                


                //methodBuilder = typeBuilder.DefineMethod($"set_{item.Key}", MethodAttributes.Public, typeof(void), new[] { item.Value });
                //iLGenerator = methodBuilder.GetILGenerator();
                //iLGenerator.Emit(OpCodes.Ldarg_0);
                //iLGenerator.Emit(OpCodes.Ldarg_1);
                //iLGenerator.Emit(OpCodes.Stfld, fieldBuilder); 
                //iLGenerator.Emit(OpCodes.Ret);
                //propertyBuild.SetSetMethod(methodBuilder);

               
            }
            ilg.Emit(OpCodes.Ret);
            Type  type=typeBuilder.CreateType();
            var fild = type.GetFields();
            var b = new { UserName = 1 };
            object o = Activator.CreateInstance(type);

            return o;
        }

        public object Test(Dictionary<string,Type>dic)
        {
           

            
                AppDomain myDomain = Thread.GetDomain();
                AssemblyName myAsmName = new AssemblyName();
                myAsmName.Name = "MyDynamicAssembly";

                AssemblyBuilder myAsmBuilder = AssemblyBuilder.DefineDynamicAssembly(
                                    myAsmName,
                                    AssemblyBuilderAccess.RunAndCollect);
                ModuleBuilder myModBuilder = myAsmBuilder.DefineDynamicModule(
                                    "MyJumpTableDemo");

                TypeBuilder myTypeBuilder = myModBuilder.DefineType("JumpTableDemo",
                                        TypeAttributes.Public);
                MethodBuilder myMthdBuilder = myTypeBuilder.DefineMethod("SwitchMe",
                                         MethodAttributes.Public |
                                         MethodAttributes.Static,
                                                         typeof(string),
                                                         new Type[] { typeof(int) });

                ILGenerator myIL = myMthdBuilder.GetILGenerator();

                Label defaultCase = myIL.DefineLabel();
                Label endOfMethod = myIL.DefineLabel();

            // We are initializing our jump table. Note that the labels
            // will be placed later using the MarkLabel method.
            Label[] jumpTable=new Label[dic.Count];
            for (int i = 0; i < dic.Count; i++)
            {
                jumpTable[i] = myIL.DefineLabel();
            }

                // arg0, the number we passed, is pushed onto the stack.
                // In this case, due to the design of the code sample,
                // the value pushed onto the stack happens to match the
                // index of the label (in IL terms, the index of the offset
                // in the jump table). If this is not the case, such as
                // when switching based on non-integer values, rules for the correspondence
                // between the possible case values and each index of the offsets
                // must be established outside of the ILGenerator.Emit calls,
                // much as a compiler would.

                myIL.Emit(OpCodes.Ldarg_0);
                myIL.Emit(OpCodes.Switch, jumpTable);
                myIL.Emit(OpCodes.Br_S, defaultCase);

            int index = 0;
            //foreach (var item in dic)
            //{
                FieldBuilder fieldBuilder = myTypeBuilder.
                   DefineField("test",
                 typeof(string), FieldAttributes.Public);
                
                myIL.MarkLabel(jumpTable[index]);
                myIL.Emit(OpCodes.Ldstr, "test");
                myIL.Emit(OpCodes.Stfld, fieldBuilder);
                myIL.Emit(OpCodes.Br_S, endOfMethod);
                index++;
            //}
           

            


            //// Branch on default case
          



            //MethodBuilder methodBuilder = myTypeBuilder.DefineMethod($"get_test_key", MethodAttributes.Public, typeof(string), null);

            //PropertyBuilder propertyBuild = myTypeBuilder.DefineProperty("Test_key", PropertyAttributes.None,typeof(string), null);

            //ILGenerator iLGenerator = methodBuilder.GetILGenerator();
            //iLGenerator.Emit(OpCodes.Ldarg_0);
            //iLGenerator.Emit(OpCodes.Ldfld, fieldBuilder);
            //iLGenerator.Emit(OpCodes.Ret);
            //propertyBuild.SetGetMethod(methodBuilder);




            //methodBuilder = myTypeBuilder.DefineMethod($"set_test_key", MethodAttributes.Public, typeof(void), new[] { typeof(string) });
            //iLGenerator = methodBuilder.GetILGenerator();
            //iLGenerator.Emit(OpCodes.Ldarg_0);
            //iLGenerator.Emit(OpCodes.Ldarg_1);
            //iLGenerator.Emit(OpCodes.Stfld, fieldBuilder); 
            //iLGenerator.Emit(OpCodes.Ret);
            //propertyBuild.SetSetMethod(methodBuilder);

            //// Case arg0 = 1
            //myIL.MarkLabel(jumpTable[1]);
            //    myIL.Emit(OpCodes.Ldstr, "is one banana");
            //iLGenerator.Emit(OpCodes.Ldfld, fieldBuilder1);
            //myIL.Emit(OpCodes.Br_S, endOfMethod);

            //    // Case arg0 = 2
            //    myIL.MarkLabel(jumpTable[2]);
            //    myIL.Emit(OpCodes.Ldstr, "are two bananas");
            //    myIL.Emit(OpCodes.Br_S, endOfMethod);

            //    // Case arg0 = 3
            //    myIL.MarkLabel(jumpTable[3]);
            //    myIL.Emit(OpCodes.Ldstr, "are three bananas");
            //    myIL.Emit(OpCodes.Br_S, endOfMethod);

            //    // Case arg0 = 4
            //    myIL.MarkLabel(jumpTable[4]);
            //    myIL.Emit(OpCodes.Ldstr, "are four bananas");
            //    myIL.Emit(OpCodes.Br_S, endOfMethod);

            //    // Default case
            //    myIL.MarkLabel(defaultCase);
            //    myIL.Emit(OpCodes.Ldstr, "are many bananas");

            //    myIL.MarkLabel(endOfMethod);
                myIL.Emit(OpCodes.Ret);
                var type=myTypeBuilder.CreateType();
            object o = Activator.CreateInstance(type);
            return o;
            }

        public object Test1(Dictionary<string, Type> dic)
        {
            AppDomain myDomain = Thread.GetDomain();
            AssemblyName myAsmName = new AssemblyName();
            myAsmName.Name = "MyDynamicAssembly";

            AssemblyBuilder myAsmBuilder = AssemblyBuilder.DefineDynamicAssembly(
                                myAsmName,
                                AssemblyBuilderAccess.Run);
            ModuleBuilder myModBuilder = myAsmBuilder.DefineDynamicModule(
                                "MyJumpTableDemo");

            TypeBuilder myTypeBuilder = myModBuilder.DefineType("JumpTableDemo",
                                    TypeAttributes.Public);
            MethodBuilder myMthdBuilder = myTypeBuilder.DefineMethod("SwitchMe",
                                     MethodAttributes.Public |
                                     MethodAttributes.Static,
                                                     typeof(string),
                                                     new Type[] { typeof(int) });

            ILGenerator myIL = myMthdBuilder.GetILGenerator();

            Label defaultCase = myIL.DefineLabel();
            Label endOfMethod = myIL.DefineLabel();
            Label[] jumpTable = new Label[] { myIL.DefineLabel(),
                      myIL.DefineLabel() };

          

            myIL.Emit(OpCodes.Ldarg_0);
            myIL.Emit(OpCodes.Switch, jumpTable);

            // Branch on default case
            myIL.Emit(OpCodes.Br_S, defaultCase);

            // Case arg0 = 0
            int index = 0;
            foreach (var item in dic)
            {
                FieldBuilder fieldBuilder = myTypeBuilder.
                  DefineField(item.Key,
                  item.Value, FieldAttributes.Public);
                myIL.MarkLabel(jumpTable[index]);
                myIL.Emit(OpCodes.Ldstr, "are no bananas");
                myIL.Emit(OpCodes.Br_S, endOfMethod);
                index++;
            }
           

          

            // Default case
            myIL.MarkLabel(defaultCase);
            myIL.Emit(OpCodes.Ldstr, "are many bananas");
            myIL.MarkLabel(endOfMethod);

            myIL.Emit(OpCodes.Ret);

            Type type = myTypeBuilder.CreateType();
            var o=Activator.CreateInstance(type);
            return o;
        }

    }
}
