import { TestBed } from "@angular/core/testing";
import { ProfileComponent } from "./profile.component";
import { RouterTestingModule } from "@angular/router/testing"
import { AuthService } from "src/app/Services/auth.service";
import { HttpClientModule } from "@angular/common/http";
import { lastValueFrom } from "rxjs";
import { JwtModule } from "@auth0/angular-jwt";
import { MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { FormsModule } from "@angular/forms";
import { ContactDetails } from "../../Models/ContactDetails.models";
import { By } from "@angular/platform-browser";
import { AddressFieldPipe } from "../../Shared/address-field.pipe";
import { Address } from "../../Models/Address.models";
import { KeysPipe } from "../../Components/table-data/keys.pipe";
import { environment } from "src/environments/environment";

export function tokenGetter() {
return localStorage.getItem('jwt');
}

describe('ProfileComponent', async()=> {
    let authService : AuthService;
    jasmine.clock().install();
    beforeEach(async()=> {
        TestBed.configureTestingModule({
            declarations:[
                ProfileComponent,
                AddressFieldPipe,
                KeysPipe
            ],
            imports:[
                HttpClientModule,
                RouterTestingModule,
                MatProgressSpinnerModule,
                FormsModule,
                JwtModule.forRoot({
                    config: {
                        tokenGetter: tokenGetter,
                        allowedDomains: ['localhost:7270'],
                        disallowedRoutes: [],
                    },
                })
            ],
            providers: [
                AuthService
            ]
        }).compileComponents();

        authService = TestBed.get(AuthService);
        let response : any = await lastValueFrom(authService.signIn(environment.testUser));
        localStorage.setItem('jwt', response.token)
        
        

    });

    it('should create the app', ()=> {
        const fixture = TestBed.createComponent(ProfileComponent);
        const app = fixture.debugElement.componentInstance;
        expect(app).toBeTruthy()
    });
    
    it('should render current user',async()=>{
        //arrange
        let fieldsToCheck = ['firstname', 'lastname', 'birthdate', 'email', 'fax', 'jobTitle']
        const fixture = TestBed.createComponent(ProfileComponent);
        await fixture.componentInstance.initUser();
        fixture.detectChanges();
        
        //act
        let contact : ContactDetails = fixture.debugElement.componentInstance.contact;
        let domElement : string = fixture.debugElement.query(By.css('.card')).nativeElement.innerText;
        
        //assert
        expect(contact).toBeTruthy()
        fieldsToCheck.forEach(key => {
            let value = contact[key];
            if (value != null)
                expect(domElement.includes(value)).toBeTruthy()
        })
        
    })

    it('should render addresses',async () => {
        //arrange
        let fieldsToCheck : string[] = ['line1', 'line2', 'country', 'stateOrProvince', 'city']
        const fixture = TestBed.createComponent(ProfileComponent);
        await fixture.componentInstance.initUser();
        fixture.detectChanges();

        //act
        let address : Address[] = fixture.componentInstance.contact.addresses;
        let addressDom : string[] = fixture.debugElement.queryAll(By.css('.address')).map(e => e.nativeElement.innerText)
        

        //assert
        expect(address.length).toEqual(3);
        for(let i=0;i<address.length;i++){
            fieldsToCheck.forEach(field =>{
                let value = address[i][field]
                if (value != null)
                    expect(addressDom[i].includes(value)).toBeTruthy();
            })
        }
        
    })

})

