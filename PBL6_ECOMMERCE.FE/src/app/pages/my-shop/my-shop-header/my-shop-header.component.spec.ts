import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MyShopHeaderComponent } from './my-shop-header.component';

describe('MyShopHeaderComponent', () => {
  let component: MyShopHeaderComponent;
  let fixture: ComponentFixture<MyShopHeaderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MyShopHeaderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MyShopHeaderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
