import { Course } from "./course";
import { CourseInstance } from "./courseinstance";

export interface CourseAndInstancesDTO {
    courses: Course[],
    courseInstances: CourseInstance[]
}