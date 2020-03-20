import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import {StockListComponent} from './stock-list/stock-list.component'
const routes: Routes = [
  {
      path: '',
      data: {
          breadcrumb: 'Vật tư'
      },
      children: [
          {
              path: 'danh-sach',
              component: StockListComponent,
              data: {
                  breadcrumb: 'Danh sách'
              }
          },
          {
              path: '',
              redirectTo: 'danh-sach',
              pathMatch: 'full'
          }
      ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class StocksRoutingModule { }
