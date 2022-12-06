use crate::core::{Day, Part};

mod d01;
mod d05;
mod d06;

pub fn get_solution(day: Day, part: Part, input: String) -> String {
    match day {
        Day::D01 => d01::solve(part, input),
        Day::D05 => d05::solve(part, input),
        Day::D06 => d06::solve(part, input),
        _ => String::new(),
    }
}
