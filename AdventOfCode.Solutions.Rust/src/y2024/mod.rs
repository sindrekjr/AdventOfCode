use crate::core::{Day, Part};

mod d01;
mod d02;
mod d03;
mod d04;
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
mod d16;
mod d17;
mod d18;
mod d19;
mod d20;
mod d21;
mod d22;
mod d23;
mod d24;
mod d25;

pub fn get_solution(day: Day, part: Part, input: String) -> Option<String> {
    match day {
        Day::D01 => d01::solve(part, input),
        Day::D02 => d02::solve(part, input),
        Day::D03 => d03::solve(part, input),
        Day::D04 => d04::solve(part, input),
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
        Day::D16 => d16::solve(part, input),
        Day::D17 => d17::solve(part, input),
        Day::D18 => d18::solve(part, input),
        Day::D19 => d19::solve(part, input),
        Day::D20 => d20::solve(part, input),
        Day::D21 => d21::solve(part, input),
        Day::D22 => d22::solve(part, input),
        Day::D23 => d23::solve(part, input),
        Day::D24 => d24::solve(part, input),
        Day::D25 => d25::solve(part, input),
    }
}
