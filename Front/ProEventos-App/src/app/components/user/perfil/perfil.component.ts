import { Component, OnInit } from '@angular/core';
import { AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from '../../../helpers/ValidatorField';

@Component({
  selector: 'app-perfil',
  templateUrl: './perfil.component.html',
  styleUrls: ['./perfil.component.scss']
})
export class PerfilComponent implements OnInit {
  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  private validation(): void {
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmarSenha')
    };

    this.form = this.fb.group({
      primeiroNome: ['',
        [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
      ],
      ultimoNome: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      titulo: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50)]],
      funcao: ['',Validators.required],
      telefone: ['', Validators.required],
      descricao: ['',
        [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
      ],
      senha: ['', [Validators.required, Validators.minLength(6)]],
      confirmarSenha: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]]
    }, formOptions);
  }

  ngOnInit(): void {
    this.validation()
  }
}
