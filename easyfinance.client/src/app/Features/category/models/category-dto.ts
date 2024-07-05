import { AutoMap } from "@automapper/classes";

export class CategoryDto {
  @AutoMap()
  id!: string;
  @AutoMap()
  name!: string;
  @AutoMap()
  goal!: number;
}
