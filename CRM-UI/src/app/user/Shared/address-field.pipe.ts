import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'addressField'
})
export class AddressFieldPipe implements PipeTransform {

  transform(value: unknown, ...args: unknown[]): unknown {
    if (value == null) return '---';
    else return value;
  }

}
