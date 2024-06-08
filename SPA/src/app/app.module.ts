import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BASE_PATH } from 'src/api_client';
import { environment } from 'src/environments/environment';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { DevUIModule } from 'ng-devui';
import { ApiModule } from 'src/api_client';

import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';


import {
  MainLayoutComponent
} from './layout/index';
import { HttpClientModule } from '@angular/common/http';
import { FaIconLibrary, FontAwesomeModule } from '@fortawesome/angular-fontawesome';

const layout = [
  MainLayoutComponent
]



@NgModule({
  declarations: [
    AppComponent,
    ...layout
  ],
  imports: [
    AppRoutingModule,
    BrowserAnimationsModule,
    DevUIModule,
    ApiModule,
    HttpClientModule,
    FontAwesomeModule
  ],
  providers: [{ provide: BASE_PATH, useValue: environment.API_BASE_PATH }],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA]
})
export class AppModule {
  constructor(lib: FaIconLibrary) {
    lib.addIconPacks(fas, far);
  }
}
