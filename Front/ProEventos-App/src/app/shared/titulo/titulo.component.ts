import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo: string | undefined;
  @Input() subTitulo: string | undefined;
  @Input() iconClass: string | undefined;
  @Input() botaoListar = false;

  constructor(private router: Router) { };

  ngOnInit(): void {
  };

  public listar(): void {
    this.router.navigate([`/${this.titulo?.toLocaleLowerCase()}/lista`]);
  };
}
