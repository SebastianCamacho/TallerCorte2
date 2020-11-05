import { Component, OnInit } from '@angular/core';
import { PersonaService } from 'src/app/Servicio/persona.service';
import { Ayudas } from '../models/ayudas';
import { Persona } from '../models/persona';

@Component({
  selector: 'app-persona-registro',
  templateUrl: './persona-registro.component.html',
  styleUrls: ['./persona-registro.component.css']
})
export class PersonaRegistroComponent implements OnInit {
  persona: Persona;
  ayuda:Ayudas;

  constructor(private personaService: PersonaService) { }

  ngOnInit(): void {
    this.persona = new Persona;
    this.ayuda=new Ayudas;
  }
  add() {
    this.persona.ayudas=this.ayuda;
    this.personaService.post(this.persona).subscribe(p => {
      if (p != null) {
        alert('Persona creada!');
        this.persona = p;
      }
    });


  }
}
