import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NzModalRef } from 'ng-zorro-antd';
import { Inventory } from '../../../../shared/models/inventory.model'
import { NotifyService } from '../../../../shared/services/notify-service'
import { MessageConstant } from '../../../../shared/constants/message-constant'
import { InventoryService } from '../../../../shared/services/inventory-service'

@Component({
  selector: 'app-inventory-modal',
  templateUrl: './inventory-modal.component.html',
  styleUrls: ['./inventory-modal.component.scss']
})


export class InventoryModalComponent implements OnInit {

  @Input() inventory: Inventory;

  @Input() isAddNew: boolean;

  inventoryForm: FormGroup;
  loadingSaveChanges: boolean;

  constructor(private fb: FormBuilder,
    private modal: NzModalRef,
    private inventoryService: InventoryService,
    private notify: NotifyService) { }

  ngOnInit() {
    this.createForm();
    this.inventoryForm.reset();
    this.inventoryForm.patchValue(this.inventory);
  }

  createForm() {
    this.inventoryForm = this.fb.group({
      id: [null],
      name: [null, [Validators.required]]
      // createdDate: [null],
      // createdBy: [null],
      // status: [null]
    });
  }

  destroyModal(){
    this.modal.destroy(false);
  }

  saveChanges(){
    const inventory = this.inventoryForm.getRawValue();
    console.log(inventory);
    if(this.isAddNew){
      this.inventoryService.addNew(inventory).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
      
    }
    else{
      this.inventoryService.update(inventory).subscribe((res: any) => {
        if (res) {
          this.notify.success(MessageConstant.CREATED_OK_MSG);
          this.modal.destroy(true);
        }

        this.loadingSaveChanges = false;
      }, _ => this.loadingSaveChanges = false);
    }

  }

}
