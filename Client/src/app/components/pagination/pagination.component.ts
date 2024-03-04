import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrl: './pagination.component.css',
})
export class PaginationComponent {
  @Input('total-items') totalItems!: number;
  @Input('page-size') pageSize = 10;
  @Output('page-changed') pageChanged = new EventEmitter();
  pages!: any[];
  currentPage = 1;

  ngOnChanges() {
    this.currentPage = 1;

    var pagesCount = Math.ceil(this.totalItems / this.pageSize);
    this.pages = [];
    for (var i = 1; i <= pagesCount; i++) this.pages.push(i);

    console.log(this);
  }

  changePage(page: any) {
    this.currentPage = page;
    this.pageChanged.emit(page);
  }

  previous() {
    if (this.currentPage == 1) return;

    this.currentPage--;
    this.pageChanged.emit(this.currentPage);
  }

  next() {
    if (this.currentPage == this.pages.length) return;

    this.currentPage++;
    console.log('next', this);
    this.pageChanged.emit(this.currentPage);
  }
}
