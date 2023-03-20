import { CourseInstance } from "./courseinstance";

export interface Course {
    id: number,
    title: string,
    duration: number,
    courseInstances: CourseInstance[]
}