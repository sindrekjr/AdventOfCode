use crate::core::{Part, Solution};

pub fn solve(part: Part, input: String) -> Option<String> {
    match part {
        Part::P1 => Day17::solve_part_one(input),
        Part::P2 => Day17::solve_part_two(input),
    }
}

struct Day17;
impl Solution for Day17 {
    fn solve_part_one(input: String) -> Option<String> {
        let ([mut a, mut b, mut c], program) = parse_input(&input);

        let mut pointer = 0;
        let mut output = vec![];
        while pointer < program.len() {
            let opcode = program[pointer];
            let literal = program[pointer + 1];
            let combo = match literal {
                0 => 0,
                1 => 1,
                2 => 2,
                3 => 3,
                4 => a,
                5 => b,
                6 => c,
                7 => 7,
                _ => panic!("illegal combo operand"),
            };

            // println!(
            //     "A: {}\nB: {}\nC: {}\nProcessing {} {}\n",
            //     a, b, c, opcode, literal
            // );

            pointer += 2;

            match opcode {
                0 => {
                    // adv
                    a /= 2u64.pow(combo as u32);
                }
                1 => {
                    // bxl
                    b ^= literal;
                }
                2 => {
                    // bst
                    b = combo % 8;
                }
                3 => {
                    // jnz
                    if a != 0 {
                        pointer = literal as usize;
                    }
                }
                4 => {
                    // bxc
                    b ^= c;
                }
                5 => {
                    // out
                    output.push(combo % 8);
                }
                6 => {
                    // bdv
                    b = a / 2u64.pow(combo as u32);
                }
                7 => {
                    // cdv
                    c = a / 2u64.pow(combo as u32);
                }
                _ => panic!("illegal opcode"),
            }
        }

        Some(
            output
                .iter()
                .map(|n| n.to_string())
                .collect::<Vec<_>>()
                .join(","),
        )
    }

    fn solve_part_two(input: String) -> Option<String> {
        let ([mut a, mut b, mut c], program) = parse_input(&input);

        let mut adjust = 0;
        loop {
            for i in 0..u64::MAX {
                let initialize = a + adjust + i;
                a += initialize;

                let mut pointer = 0;
                let mut output = vec![];
                while pointer < program.len() {
                    let opcode = program[pointer];
                    let literal = program[pointer + 1];
                    let combo = match literal {
                        0 => 0,
                        1 => 1,
                        2 => 2,
                        3 => 3,
                        4 => a,
                        5 => b,
                        6 => c,
                        7 => 7,
                        _ => panic!("illegal combo operand"),
                    };

                    pointer += 2;

                    match opcode {
                        0 => {
                            // adv
                            a /= 2u64.pow(combo as u32);
                        }
                        1 => {
                            // bxl
                            b ^= literal;
                        }
                        2 => {
                            // bst
                            b = combo % 8;
                        }
                        3 => {
                            // jnz
                            if a != 0 {
                                pointer = literal as usize;
                            }
                        }
                        4 => {
                            // bxc
                            b ^= c;
                        }
                        5 => {
                            // out
                            output.push(combo % 8);
                        }
                        6 => {
                            // bdv
                            b = a / 2u64.pow(combo as u32);
                        }
                        7 => {
                            // cdv
                            c = a / 2u64.pow(combo as u32);
                        }
                        _ => panic!("illegal opcode"),
                    }
                }

                if output == program {
                    return Some(initialize.to_string());
                } else if output.ends_with(&program[program.len() - output.len()..]) {
                    adjust = (adjust + i) * 8;
                    break;
                }
            }
        }
    }
}

fn parse_input(input: &str) -> ([u64; 3], Vec<u64>) {
    let (registers, program) = input.split_once("\n\n").unwrap();
    (
        registers
            .lines()
            .map(|line| {
                let (_, value) = line.split_once(": ").unwrap();
                value.parse().unwrap()
            })
            .collect::<Vec<_>>()
            .try_into()
            .unwrap(),
        program
            .split_once(": ")
            .unwrap()
            .1
            .split(',')
            .map(|n| n.parse().unwrap())
            .collect(),
    )
}
