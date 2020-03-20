import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';


const routes: Routes = [
  {
    path: '',
    data: {
        breadcrumb: 'Danh mục'
    },
    children: [
        {
            path: 'don-vi-tinh',
            loadChildren: () => import('./units/units.module').then(m => m.UnitsModule)
        },
        {
            path: 'kho',
            loadChildren: () => import('./inventories/inventories.module').then(m => m.InventoriesModule)
        },
        {
            path: 'vat-tu',
            loadChildren: () => import('./stocks/stocks.module').then(m => m.StocksModule)
        },
        {
            path: 'khach-hang',
            loadChildren: () => import('./customers/customers.module').then(m => m.CustomersModule)
        },
        {
            path: '',
            redirectTo: 'vat-tu',
            pathMatch: 'full'
        }
    ]
}
];
@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CategoriesRoutingModule { }


