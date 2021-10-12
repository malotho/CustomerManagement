import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';

import { AlertService, CategoryService } from 'src/app/_services';
import { Category } from '../_models';

@Component({ templateUrl: 'add-edit.component.html' })
export class AddEditComponent implements OnInit {
  form: FormGroup;

  categories = null;
  selectedCategory: Category;

  id: string;
  isAddMode: boolean;
  loading = false;
  submitted = false;

  //catCodePattern = "[A-Za-z]";

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private categoryService: CategoryService,
    private alertService: AlertService
  ) { }


  onCategoryChanged(event: any) {
    this.selectedCategory = this.categories.find(n => n.categoryName == event.target.value);
    this.setFormControlValues(this.selectedCategory.id);
  }

  setFormControlValues(catId: number) {
    if (catId == 0) {
      this.form.get('category').setValue("Select Category");
    }
    else {
      this.selectedCategory = this.categories.find(i => i.id == catId);
      this.form.get('category').setValue(this.selectedCategory.categoryName);
    }
  }
  ngOnInit() {

    this.categoryService.getAll()
      .pipe(first())
      .subscribe(categories => this.categories = categories);

    this.id = this.route.snapshot.params['id'];
    this.isAddMode = !this.id;

    this.form = this.formBuilder.group({
      id: 0,
      categoryName: ['', Validators.required],
      categoryCode: ['', Validators.required],
      isActive: [false]
    });

    if (!this.isAddMode) {
      this.categoryService.getById(this.id)
        .pipe(first())
        .subscribe(x => {
          this.f.id.setValue(x.id);
          this.f.categoryName.setValue(x.categoryName);
          this.f.categoryCode.setValue(x.categoryCode);
          this.f.isActive.setValue(x.isActive);
        });
    }
  }

  // convenience getter for easy access to form fields
  get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;

    // reset alerts on submit
    this.alertService.clear();

    // stop here if form is invalid
    if (this.form.invalid) {
      return;
    }

    this.loading = true;
    if (this.isAddMode) {
      this.createCategory();
    } else {
      this.updateCategory();
    }
  }

  private createCategory() {
    this.categoryService.add(this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          this.alertService.success('Category added!', { keepAfterRouteChange: true });
          this.router.navigate(['.', { relativeTo: this.route }]);
        },
        error => {
          console.log(error)
          this.alertService.error(error);
          this.loading = false;
        });
  }

  private updateCategory() {
    console.log(this.form.value)
    this.categoryService.update(this.id, this.form.value)
      .pipe(first())
      .subscribe(
        data => {
          console.log(data)
          this.alertService.success('Category Updated!', { keepAfterRouteChange: true });
          this.router.navigate(['..', { relativeTo: this.route }]);
        },
        error => {
          this.alertService.error(error);
          this.loading = false;
        });
  }
}
