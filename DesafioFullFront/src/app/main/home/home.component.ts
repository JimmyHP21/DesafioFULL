import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { HomeService } from './service/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  form: any;
  totalValue = 0;
  totalParcel = 0;

  headers = ["Número do título", "Nome do devedor", "Qtde de parcelas", "Valor original", "Dias em atraso", "Valor atualizado"];

  rows: any;

  constructor(private _formBuilder: FormBuilder, private datePipe: DatePipe,
              private _homeService: HomeService) { }

  ngOnInit(): void {
    this.form = this.createForm();

    this.getList();
  }

  getList() {
    this._homeService.titleList().subscribe((response) => {
      this.rows = response;
    }, (error) => console.log(error))
  }

  createForm(): FormGroup {
    return this._formBuilder.group({
      TitleNumber: ['', Validators.required],
      NameDebtor: ['', Validators.required],
      CpfDebtor: ['', Validators.required],
      PercentFees: ['', Validators.required],
      PercentFine: ['', Validators.required],
      DebtInstallments : this._formBuilder.array([
        new FormGroup({
          NumberPart: new FormControl(),
          dueDate: new FormControl(),
          valuePart: new FormControl
        })
      ])
    })
  }

  onSubmit() {
    this.parcelMethod(this.totalValue, this.totalParcel);
    this._homeService.titlePost(this.form.value).subscribe((response) => {
      this.getList();
    }, (error) => {
      console.log(error)
    })
  }

  parcelMethod(max: number, parcel: number) {
    const dbtArray = this.form.get('DebtInstallments') as FormArray;
    dbtArray.clear();
    const myDate = new Date();

    for(let i = 0; i < parcel; i++) {
      dbtArray.push(
        this.addParcel(i+1, this.datePipe.transform(new Date(myDate.getFullYear(), myDate.getMonth() + (i + 1), myDate.getDate()), "yyyy-MM-ddT00:00:00"), +(max/parcel).toFixed(2))
      );
    }
  }

  totalValueMethod(list: any) {
    let total = 0;
    list.map((item: any) => {
      total += item.valuePart;
    });

    return total.toFixed(2);
  }

  totalValueAtrasoMethod(juros: number, multa:number, list: any) {
    let multaPercent = (multa / 100).toFixed(2);
    let totalJuros = 0;
    let total = 0;
    list.map((item: any) => {
      total += item.valuePart;
      let days = this.getDays(item.dueDate) || 0 ;
      totalJuros += (juros / 30) * +days * item.valuePart;
    });

    total = total + (total * +multaPercent)
    return (total + +totalJuros).toFixed(2);
  }

  getDays(due: any) {
    const now = new Date();
    const da = new Date(due)

    const final = Math.abs( now.getTime() - da.getTime() );
    return Math.ceil( final / (1000 * 3600 * 24))
  }

  days(list: any) {
    const now = new Date();
    const da = new Date(list[0] ? list[0].dueDate : 0)

    const final = Math.abs( now.getTime() - da.getTime() );
    return Math.ceil( final / (1000 * 3600 * 24))
  }

  addParcel(number: any, date: any, value: any): FormGroup {
    return new FormGroup({
      'NumberPart': new FormControl(number),
      'dueDate': new FormControl(date),
      'valuePart': new FormControl(value)
    })
  }
}
