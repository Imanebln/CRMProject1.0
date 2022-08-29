import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Opportunity } from '../Pages/opportunities/opportunities.model';

@Injectable({
  providedIn: 'root',
})
export class OpportunityService {
  constructor(private http: HttpClient) {}

  apiUrl: string = environment.apiUrl;

  getOpportunitys() {
    return this.http.get<Opportunity[]>(this.apiUrl + 'Opportunitys');
  }

  getOpportunitysId(opportunityId: string): Observable<any> {
    return this.http.get<Opportunity>(
      this.apiUrl + 'Opportunitys/' + opportunityId
    );
  }

  getOpportunityByEmail(email: string): Observable<any> {
    return this.http.get<Opportunity>(
      this.apiUrl + 'GetOpportunityByEmail/' + email
    );
  }
}
