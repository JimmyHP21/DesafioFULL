import { HttpClient, HttpErrorResponse } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { throwError } from "rxjs";
import { retry, catchError } from "rxjs/operators";
import { environment } from "../../../../environments/environment";

@Injectable({ providedIn: "root"})
export class HomeService {
  url = environment.url;

  headers = {
    'Expires': '0',
    'Pragma': 'no-cache',
    'Content-Type': 'application/json',
    'Cache-Control': 'no-cache, no-store, must-revalidate',
    'Access-Control-Allow-Headers': 'Access-Control-Allow-Origin, Content-Type, Accept, Accept-Language, Origin, User-Agent',
    'Access-Control-Allow-Origin': '*',
    'Access-Control-Allow-Methods': 'GET, POST, PUT, DELETE, OPTIONS'
  };

   /**
   * Constructor
   *
   * @param {HttpClient} _httpClient
   */
    constructor( private _httpClient: HttpClient ) { }

    titleList() {
      return this._httpClient.get('/api/TitleDelay', {headers: this.headers})
        .pipe(
          retry(1),
          catchError(this.handleError)
        );
    }

    titlePost(data: any) {
      return this._httpClient.post('/api/TitleDelay/new', data, {headers: this.headers})
        .pipe(
          retry(1),
          catchError(this.handleError)
        );
    }

    // Manipulação de erros
  handleError(error: HttpErrorResponse): any {
    let errorMessage: string;
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}

