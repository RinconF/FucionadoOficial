using System;
using System.Collections;

namespace DCL
{
    public class Int_TutorialesCollection : CollectionBase
    {
        public Int_Tutoriales this[int index]
        {
            get { return (Int_Tutoriales)List[index]; }
            set { List[index] = value; }
        }

        public int Add(Int_Tutoriales value)
        {
            return List.Add(value);
        }

        public int IndexOf(Int_Tutoriales value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, Int_Tutoriales value)
        {
            List.Insert(index, value);
        }

        public void Remove(Int_Tutoriales value)
        {
            List.Remove(value);
        }

        public bool Contains(Int_Tutoriales value)
        {
            return List.Contains(value);
        }

        protected override void OnValidate(object value)
        {
            if (value.GetType() != Type.GetType("DCL.Int_Tutoriales"))
                throw new ArgumentException("No se permiten valores de tipo diferente a Int_Tutoriales", "value");
        }
    }
}
