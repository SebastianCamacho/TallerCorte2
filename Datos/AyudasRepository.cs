using System.Linq;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity;

namespace Datos
{
    public class AyudasRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Ayudas> _Ayudas = new List<Ayudas>();
        public AyudasRepository(ConnectionManager connection)
        {
            _connection = connection._conexion;
        }

        

        public void Guardar(Ayudas ayuda)
        {
            
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Ayuda (ValorApoyo,ModalidadApoyo, Fecha) 
                                        values (@ValorA,@ModalidadA,@Fecha)";
                command.Parameters.AddWithValue("@ValorA", ayuda.ValorApoyo);
                command.Parameters.AddWithValue("@ModalidadA", ayuda.ModalidadApoyo);
                command.Parameters.AddWithValue("@Fecha", ayuda.Fecha);
                
                var filas = command.ExecuteNonQuery();
            }
        }
        public int calcularApoyoId(){
            if(!ConsultarAyudas().Any()){
                return 1;
            }else{
            Ayudas Ultimo=ConsultarAyudas().Last();
            int n=Ultimo.ApoyoId+1;
            return n;
            }
        }

        public List<Ayudas> ConsultarAyudas()
        {
            SqlDataReader dataReader;
            List<Ayudas> ayudas = new List<Ayudas>();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from Ayuda ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Ayudas ayuda = DataReaderMapToPerson(dataReader);
                        ayudas.Add(ayuda);
                    }
                }
            }
            return ayudas;
        }
         private Ayudas DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Ayudas ayuda = new Ayudas();
            ayuda.ApoyoId = (int)dataReader["ApoyoId"];
            ayuda.ValorApoyo = (decimal)dataReader["ValorApoyo"];
            ayuda.ModalidadApoyo = (string)dataReader["ModalidadApoyo"];
            ayuda.Fecha = (DateTime)dataReader["Fecha"];
            return ayuda;
        }


    }
        
}