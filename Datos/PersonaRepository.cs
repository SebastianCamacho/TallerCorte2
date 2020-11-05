using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Entity;

namespace Datos
{
    public class PersonaRepository
    {
        private readonly SqlConnection _connection;
        private readonly List<Persona> _personas = new List<Persona>();
        private readonly AyudasRepository ayudasRepositorio;
        public PersonaRepository(ConnectionManager connection){
            _connection = connection._conexion;
            ayudasRepositorio=new AyudasRepository(connection);
        }
        public void Guardar(Persona persona)
        {
            persona.Ayuda.ApoyoId=ayudasRepositorio.calcularApoyoId();
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = @"Insert Into Persona (Identificacion,Nombre,Apellido,Sexo,Edad,Departamento,Ciudad,ApoyoId) 
                                        values (@Identificacion,@Nombre,@Apellido,@Sexo,@Edad,@Departamento,@Ciudad,@ApoyoId)";
                command.Parameters.AddWithValue("@Identificacion", persona.Identificacion);
                command.Parameters.AddWithValue("@Nombre", persona.Nombre);
                command.Parameters.AddWithValue("@Apellido", persona.Apellidos);
                command.Parameters.AddWithValue("@Sexo", persona.Sexo);
                command.Parameters.AddWithValue("@Edad", persona.Edad);
                command.Parameters.AddWithValue("@Departamento", persona.Departamento);
                command.Parameters.AddWithValue("@Ciudad", persona.Ciudad);
                command.Parameters.AddWithValue("@ApoyoId", persona.Ayuda.ApoyoId);
                var filas = command.ExecuteNonQuery();
            }
            ayudasRepositorio.Guardar(persona.Ayuda);
        }

        public List<Persona> ConsultarPersonas()
        {
            SqlDataReader dataReader;
            List<Persona> personas = new List<Persona>();
            
            using (var command = _connection.CreateCommand())
            {
                command.CommandText = "Select * from persona ";
                dataReader = command.ExecuteReader();
                if (dataReader.HasRows)
                {
                    while (dataReader.Read())
                    {
                        Persona persona = DataReaderMapToPerson(dataReader);
                        personas.Add(persona);
                    }
                }
            }
            List<Ayudas> ayudas=ayudasRepositorio.ConsultarAyudas();
            foreach (var a in ayudas)
            {
                foreach (var p in personas)
                {
                    if(p.Ayuda.ApoyoId==a.ApoyoId){
                        p.Ayuda=a;
                    }
                }
            }

            return personas;
        }

        private Persona DataReaderMapToPerson(SqlDataReader dataReader)
        {
            if(!dataReader.HasRows) return null;
            Persona persona = new Persona();
            persona.Identificacion = (string)dataReader["Identificacion"];
            persona.Nombre = (string)dataReader["Nombre"];
            persona.Apellidos = (string)dataReader["Apellido"];
            persona.Sexo = (string)dataReader["Sexo"];
            persona.Edad = (int)dataReader["Edad"];
            persona.Departamento = (string)dataReader["Departamento"];
            persona.Ciudad = (string)dataReader["Ciudad"];
            persona.Ayuda.ApoyoId=(int)dataReader["ApoyoId"];
            return persona;
        }

    }
}
