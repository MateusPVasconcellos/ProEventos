import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss']
})
export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;

  get f(): any {
    return this.form.controls;
  }

  constructor(private fb: FormBuilder) { }

  private validation(): void {
    this.form = this.fb.group({
      local: ['',
        [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
      ],
      dataEvento: ['', Validators.required],
      tema: ['',
        [Validators.required, Validators.minLength(4), Validators.maxLength(50)]
      ],
      qtdPessoas: ['',
        [Validators.required, Validators.pattern("^[0-9]*$"), Validators.max(1200)]
      ],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.email, Validators.required]]
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  ngOnInit(): void {
    this.validation();
  }

}
