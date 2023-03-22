import { Course } from "./course";

export interface CourseInstance {
    id: number, 
    startDate: string,
    courseId: number,
    course: Course
}