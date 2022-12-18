use crate::core::{Day, Part};

pub mod coor;

mod d01;
mod d05;
mod d06;
mod d07;
mod d08;
mod d09;
mod d10;
mod d11;
mod d12;
mod d13;
mod d14;
mod d15;
mod d18;

pub fn get_solution(day: Day, part: Part, input: String) -> String {
    match day {
        Day::D01 => d01::solve(part, input),
        Day::D05 => d05::solve(part, input),
        Day::D06 => d06::solve(part, input),
        Day::D07 => d07::solve(part, input),
        Day::D08 => d08::solve(part, input),
        Day::D09 => d09::solve(part, input),
        Day::D10 => d10::solve(part, input),
        Day::D11 => d11::solve(part, input),
        Day::D12 => d12::solve(part, input),
        Day::D13 => d13::solve(part, input),
        Day::D14 => d14::solve(part, input),
        Day::D15 => d15::solve(part, input),
        Day::D18 => d18::solve(part, input),
        _ => String::new(),
    }
}
