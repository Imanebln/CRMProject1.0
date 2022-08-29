import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'headerTable',
})
export class HeaderTablePipe implements PipeTransform {
  transform(value: string, ...args: unknown[]) {
    if (value == 'email') {
      return 'Email';
    } else if (value == 'birthdate') {
      return 'Birth Date';
    } else if (value == 'jobTitle') {
      return 'Job Title';
    } else if (value == 'totalAmount') {
      return 'Total Amount';
    } else if (value == 'createdOn') {
      return 'Created On';
    } else if (value == 'currentSituation') {
      return 'current Situation';
    } else if (value == 'proposedSolution') {
      return 'proposed Solution';
    }
    return value;
  }
}
