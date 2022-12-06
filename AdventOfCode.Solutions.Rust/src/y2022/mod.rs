use crate::core::Part;

mod d01;
mod d05;
mod d06;

pub fn get_solution(day: u8, part: Part, input: String) -> String {
    match day {
        1 => d01::solve(part, input),
        5 => d05::solve(part, input),
        6 => d06::solve(part, input),
        _ => String::new()
    }
}
