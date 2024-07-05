import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListCategoriesComponent } from './list-categories.component';

describe('ListCategoriesComponent', () => {
  let component: ListCategoriesComponent;
  let fixture: ComponentFixture<ListCategoriesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ListCategoriesComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ListCategoriesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
